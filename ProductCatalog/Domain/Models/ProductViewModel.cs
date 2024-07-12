using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Models
{
    public record ProductViewModel(
        string Id,
        string Name,
        string Code,
        string Description,
        Money Price,
        bool Available);
}
