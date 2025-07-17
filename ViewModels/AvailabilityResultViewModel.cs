namespace BoutiqueManagement.ViewModels
{
    public class AvailabilityResultViewModel
    {
        public string ItemCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool HasSearched { get; set; }

        public bool NotFound { get; set; }
        public bool InvalidDateRange { get; set; }
        public bool IsAvailable { get; set; }
        public string ItemName { get; set; }
    }
    

}
