namespace ShoppingCart.Domain.Models
{
    public record CartPostModel
    {
        public string UserId { get; set; }

        public IEnumerable<string> ProductIds { get; set; }
    }
}
