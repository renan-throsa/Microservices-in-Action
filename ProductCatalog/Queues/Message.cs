using System.Text;
using System.Text.Json;

namespace ProductCatalog.Queues
{
    public abstract class Message
    {
        public string CorrelationId { get; set; }

        public abstract string Subject { get; }

        public Message()
        {
            CorrelationId = Guid.NewGuid().ToString();
        }

        public byte[] ToData()
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.SerializeToUtf8Bytes(this, option);
        }

        public static Message FromData(byte[] data)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<Message>(data, option);
        }
    }
}
