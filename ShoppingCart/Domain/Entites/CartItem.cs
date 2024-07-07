namespace ShoppingCart.Domain.Entites
{
    public record CartItem(string ProductCatalogueId, string ProductName, string Description, Money Price)
    {
        public virtual bool Equals(CartItem? obj)
        {
            return obj != null && ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
            return ProductCatalogueId.GetHashCode();
        }
    }

    public record Money(string Currency, decimal Amount);
}
