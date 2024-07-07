namespace ShoppingCart.Domain.Models
{
    public class CartPostModel
    {
        public string UserId { get; set; }

        public string[] ProductIds { get; set; }
    }
}
