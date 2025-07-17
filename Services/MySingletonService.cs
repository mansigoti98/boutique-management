namespace BoutiqueManagement.Services
{
    public class MySingletonService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }

}
