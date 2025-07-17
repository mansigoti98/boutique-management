using BoutiqueManagement.Data;
using BoutiqueManagement.Models;
using BoutiqueManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class BoutiqueController : Controller
{
    private readonly AppDbContext _context;
    public BoutiqueController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalItems = await _context.BoutiqueItems.CountAsync();
        ViewBag.TotalRentals = await _context.BoutiqueItems.CountAsync(x => x.IsRental);
        ViewBag.TotalSales = await _context.BoutiqueItems.CountAsync(x => !x.IsRental);

        ViewBag.MostPopular = await _context.BoutiqueItems
            .GroupJoin(_context.RentalTransactions,
                       i => i.ItemCode, t => t.ItemCode,
                       (item, trans) => new { Item = item, Count = trans.Count() })
            .OrderByDescending(ic => ic.Count)
            .Take(5)
            .Select(ic => ic.Item)
            .ToListAsync();

        return View();
    }

    public async Task<IActionResult> Rentals() =>
        View(await _context.BoutiqueItems.Where(x => x.IsRental).ToListAsync());

    public async Task<IActionResult> Sales() =>
        View(await _context.BoutiqueItems.Where(x => !x.IsRental).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadCategories();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BoutiqueItem item, IFormFile imageFile)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(item);
        }

        if (imageFile != null && imageFile.Length > 0)
        {
            if (imageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("", "File too large. Max allowed is 2 MB.");
                await LoadCategories();
                return View(item);
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            item.ImageUrl = "/images/" + uniqueFileName;
        }

        _context.BoutiqueItems.Add(item);
        await _context.SaveChangesAsync();

        return RedirectToAction(item.IsRental ? "Rentals" : "Sales");
    }

    [HttpGet]
    public async Task<IActionResult> BookRental(string itemCode)
    {
        if (string.IsNullOrEmpty(itemCode))
            return NotFound();

        var item = await _context.BoutiqueItems.FirstOrDefaultAsync(x => x.ItemCode == itemCode);
        if (item == null)
            return NotFound();

        var txn = new RentalTransaction
        {
            ItemCode = itemCode,
            BoutiqueItem = item,
            RentalStartDate = DateTime.Today,
            RentalEndDate = DateTime.Today.AddDays(5)
        };

        return View(txn);
    }

    [HttpPost]
    public async Task<IActionResult> BookRental(RentalTransaction txn)
    {
        if (txn.RentalStartDate < DateTime.Today || txn.RentalEndDate < DateTime.Today || txn.RentalStartDate > txn.RentalEndDate)
        {
            ModelState.AddModelError("", "Invalid dates. Please check start and end dates.");
        }

        txn.BoutiqueItem = await _context.BoutiqueItems.FirstOrDefaultAsync(x => x.ItemCode == txn.ItemCode);
        if (txn.BoutiqueItem == null)
        {
            ModelState.AddModelError("", "Selected item no longer exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(txn);
        }

        var conflicts = await _context.RentalTransactions
            .AnyAsync(r => r.ItemCode == txn.ItemCode &&
                ((txn.RentalStartDate >= r.RentalStartDate && txn.RentalStartDate <= r.RentalEndDate) ||
                 (txn.RentalEndDate >= r.RentalStartDate && txn.RentalEndDate <= r.RentalEndDate) ||
                 (txn.RentalStartDate <= r.RentalStartDate && txn.RentalEndDate >= r.RentalEndDate)));

        if (conflicts)
        {
            ModelState.AddModelError("", "This item is already booked for those dates.");
            return View(txn);
        }

        txn.BookedOn = DateTime.Now;
        txn.RentalPrice = txn.BoutiqueItem.RentalPrice;

        _context.RentalTransactions.Add(txn);
        await _context.SaveChangesAsync();

        return RedirectToAction("Invoice", new { id = txn.Id });
    }

    public async Task<IActionResult> Invoice(int id)
    {
        var invoice = await _context.RentalTransactions
            .Include(x => x.BoutiqueItem)
            .FirstOrDefaultAsync(x => x.Id == id);

        return invoice == null ? NotFound() : View(invoice);
    }

    [HttpGet]
    public IActionResult CheckAvailability()
    {
        return View(new AvailabilityResultViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CheckAvailability(string itemCode, DateTime startDate, DateTime endDate)
    {
        var model = new AvailabilityResultViewModel
        {
            ItemCode = itemCode,
            StartDate = startDate,
            EndDate = endDate,
            HasSearched = true
        };

        if (string.IsNullOrEmpty(itemCode))
            return View(model);

        var item = await _context.BoutiqueItems.FirstOrDefaultAsync(x => x.ItemCode == itemCode);
        if (item == null)
        {
            model.NotFound = true;
            return View(model);
        }

        if (startDate < DateTime.Today || endDate < DateTime.Today || startDate > endDate)
        {
            model.InvalidDateRange = true;
            model.ItemName = item.Name;
            return View(model);
        }

        var isBooked = await _context.RentalTransactions
            .AnyAsync(r => r.ItemCode == itemCode &&
                 ((startDate >= r.RentalStartDate && startDate <= r.RentalEndDate) ||
                  (endDate >= r.RentalStartDate && endDate <= r.RentalEndDate) ||
                  (startDate <= r.RentalStartDate && endDate >= r.RentalEndDate)));

        model.ItemName = item.Name;
        model.IsAvailable = !isBooked;

        return View(model);
    }

    // DRY: centralize dropdown loading
    private async Task LoadCategories()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
    }
}
