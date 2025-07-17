namespace BoutiqueManagement.Services
{
    public class MyTransientService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}
