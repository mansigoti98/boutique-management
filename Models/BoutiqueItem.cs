namespace BoutiqueManagement.Models
{
    public class BoutiqueItem
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ItemCode { get; set; } // 4-digit product code
        public string? ImageUrl { get; set; }
        public decimal RentalPrice { get; set; }
        public bool IsRental { get; set; }

            public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
