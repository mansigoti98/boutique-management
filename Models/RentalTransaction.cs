using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoutiqueManagement.Models
{
    public class RentalTransaction
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string CustomerName { get; set; }

        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public DateTime BookedOn { get; set; }
        public decimal RentalPrice { get; set; }
        [BindNever]
        public BoutiqueItem? BoutiqueItem { get; set; }
    }
}