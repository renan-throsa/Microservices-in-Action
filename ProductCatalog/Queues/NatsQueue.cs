using Microsoft.Extensions.Options;
using NATS.Client.Core;
using ProductCatalog.Utils;

namespace ProductCatalog.Queues
{
    public class NatsQueue : IQueue, IDisposable
    {
        private readonly NatsConnection _connection;

        public NatsQueue(IOptions<QueueSettings> options)
        {
            NatsOpts opts = new() { Url = options.Value.Url };
            _connection = new NatsConnection(opts);
        }

        public async Task Publish(string sub, string data)
        {
            await _connection.PublishAsync(sub, data);
        }

        public void Dispose()
        {
            _connection.DisposeAsync();
        }

    }
}
