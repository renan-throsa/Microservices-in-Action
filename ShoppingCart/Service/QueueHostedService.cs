
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Queues;
using ShoppingCart.Utils;
using System.Text.Json;

namespace ShoppingCart.Service
{
    public class QueueHostedService : BackgroundService
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly NatsConnection _connection;
        private readonly ILogger<IShoppingCartService> _logger;

        public QueueHostedService(IShoppingCartService shoppingCartService, IOptions<QueueSettings> options, ILogger<IShoppingCartService> logger)
        {
            _shoppingCartService = shoppingCartService;
            _connection = new NatsConnection(new NatsOpts() { Url = options.Value.Url });
            _logger = logger;
            _logger.LogInformation($"Queue hosted service woker initiated and listening to {options.Value.Url}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sub = await _connection.SubscribeCoreAsync<string>(subject: ProductChangedEvent.MessageSubject);

            while (!stoppingToken.IsCancellationRequested)
            {
                var msg = await sub.Msgs.ReadAsync();
                _logger.LogInformation($"Received: {msg.Data}");
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                };

                var change = JsonSerializer.Deserialize<ProductChangedEvent>(msg.Data, option);
                
                if (change != null)
                {
                    await _shoppingCartService.UpdatedAsync(change.ProductId);
                }
                
            }

            await sub.UnsubscribeAsync();
        }

    }
}
