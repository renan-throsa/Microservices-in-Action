using ShoppingCart.Domain.Entites;

namespace ShoppingCart.Domain.Models
{
    public class CartItemViewModel
    {
        public string ProductCatalogueId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }

    }

    
}
