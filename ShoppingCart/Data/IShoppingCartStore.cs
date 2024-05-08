namespace ShoppingCart.Data
{
    public interface IShoppingCartStore
    {
        Domain.ShoppingCart GetBy(int userId);
        void Save(Domain.ShoppingCart shoppingCart);
    }
}
