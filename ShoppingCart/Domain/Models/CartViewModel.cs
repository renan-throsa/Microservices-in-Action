namespace ShoppingCart.Domain.Models
{
    public class CartViewModel
    {
        public string ShoppingCartId { get; }

        public string UserId { get; }

        private readonly HashSet<CartItemViewModel> items = new();

        public IEnumerable<CartItemViewModel> Items => items;

        
    }
}
