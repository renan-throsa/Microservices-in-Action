using Microsoft.Extensions.Options;
using NATS.Client;
using ProductCatalog.Utils;

namespace ProductCatalog.Queues
{
    public class NatsQueue : IQueue
    {
        private readonly IConnection _connection;

        public NatsQueue(IOptions<QueueSettings> options)
        {
            _connection = new ConnectionFactory().CreateConnection(options.Value.Url);
        }

        public void Publish(Message message)
        {
            _connection.Publish(message.Subject, message.ToData());
        }
    }
}
