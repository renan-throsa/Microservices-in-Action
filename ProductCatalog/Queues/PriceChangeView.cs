using MongoDB.Bson;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Queues
{
    public record PriceChangeView(string Id, Money Before, Money After);
}
