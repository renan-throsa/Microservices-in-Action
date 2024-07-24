namespace ClientGateway.Domain.Models
{
    public record ProductPatchModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MoneyModel Price { get; set; }
    }
}
