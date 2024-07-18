using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Models
{
    public record ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }
        public bool Available { get; set; }
    }
}
