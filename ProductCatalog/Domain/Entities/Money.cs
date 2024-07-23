namespace ProductCatalog.Domain.Entities
{
    public record Money(string Currency, decimal Amount)
    {
        public virtual bool Equals(Money? obj)
        {
            return obj != null && obj.Amount == Amount && obj.Currency == Currency;
        }

    }
}
