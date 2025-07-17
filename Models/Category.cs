namespace BoutiqueManagement.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BoutiqueItem> BoutiqueItems { get; set; }
    }

}
