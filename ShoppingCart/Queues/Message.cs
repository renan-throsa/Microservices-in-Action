using System.Text.Json;

namespace ShoppingCart.Queues
{
    public abstract class Message
    {
        public string CorrelationId { get; set; }

        public abstract string Subject { get; }

        public Message()
        {
            CorrelationId = Guid.NewGuid().ToString();
        }        
    }
}
