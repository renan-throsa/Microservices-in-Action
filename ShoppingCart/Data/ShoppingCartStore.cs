
namespace ShoppingCart.Data
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private readonly Dictionary<int, Domain.ShoppingCart> _database;

        public ShoppingCartStore()
        {
            _database = new Dictionary<int, Domain.ShoppingCart>();
        }

        public Domain.ShoppingCart GetBy(int userId)
        {
            return _database.ContainsKey(userId) ? _database[userId] : new Domain.ShoppingCart(userId);
        }

        public void Save(Domain.ShoppingCart shoppingCart)
        {
            _database[shoppingCart.UserId] = shoppingCart;
        }
    }
}
