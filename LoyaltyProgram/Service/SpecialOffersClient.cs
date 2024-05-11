using LoyaltyProgram.Data;
using Quartz;
using System.Net.Http.Headers;
using System.Text.Json;
using LoyaltyProgram.Domain;
using Microsoft.Extensions.Options;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Service
{
    [DisallowConcurrentExecution]
    public class SpecialOffersClient : ISpecialOffersClient, IJob
    {
        private readonly IEventStore _store;
        private readonly ILogger<SpecialOffersClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly ClientSettings _clientSettings;
        public SpecialOffersClient(IEventStore store, ILogger<SpecialOffersClient> logger, HttpClient httpClient, IOptions<ClientSettings> options)
        {
            _store = store;
            _httpClient = httpClient;
            _logger = logger;
            _clientSettings = options.Value;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Fetching latest events");
            var start = _store.GetStartIdFromDatastore();
            var end = 5;
            var resp = await _httpClient.GetAsync(new Uri($"{_clientSettings.Route}?start={start}&end={end}"));
            if (resp.IsSuccessStatusCode)
            {
                using var stream = await resp.Content.ReadAsStreamAsync();
                await ProcessEvents(stream);
                _logger.LogInformation("Latest events updated");
            }
            else
            {
                _logger.LogError($"Request failed with status code: {resp.StatusCode}");

            }

        }


        public async Task ProcessEvents(Stream content)
        {
            IAsyncEnumerable<Event?> events = JsonSerializer.DeserializeAsyncEnumerable<Event?>(content);
            await foreach (Event? specialOffer in events)
            {
                if (specialOffer is not null)
                {
                    _store.Add(specialOffer);
                    _logger.LogInformation($"{specialOffer.Name} event added");
                }

            }

        }

    }
}
