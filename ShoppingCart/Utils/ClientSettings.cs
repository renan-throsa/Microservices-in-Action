namespace ShoppingCart.Utils
{
    public sealed class ClientSettings
    {
        public string BaseAddress { get; set; }

        public ClientSettings()
        {
            BaseAddress = string.Empty;
        }
    }
}
