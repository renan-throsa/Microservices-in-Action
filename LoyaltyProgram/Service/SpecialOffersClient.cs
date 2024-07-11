using Quartz;
using System.Text.Json;
using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;

namespace LoyaltyProgram.Service
{
    [DisallowConcurrentExecution]
    public class SpecialOffersClient : ISpecialOffersClient, IJob
    {
        private readonly IEventRepository _store;
        private readonly ILogger<SpecialOffersClient> _logger;
        private readonly IHttpClientFactory _factory;

        public SpecialOffersClient(IEventRepository store, ILogger<SpecialOffersClient> logger, IHttpClientFactory factory)
        {
            _store = store;
            _factory = factory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Fetching latest events");
            var start = _store.GetNextSequencyEventNumber();
            var end = 5;

            HttpResponseMessage resp;

            try
            {
                resp = await _factory.CreateClient("events").GetAsync($"?start={start}&end={end}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return;
            }


            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                ProcessEvents(content);
                _logger.LogInformation("Latest events updated");
            }
            else
            {
                _logger.LogError($"Request failed with status code: {resp.StatusCode}");

            }

        }

        public void ProcessEvents(string content)
        {

            var events = JsonSerializer.Deserialize<List<Event>>(content);

            if (events is null)
            {
                _logger.LogInformation($"No event fetched");
                return;
            }

            foreach (Event specialOffer in events)
            {
                if (specialOffer is not null)
                {
                    _store.Add(specialOffer);
                    _logger.LogInformation($"{specialOffer.Name} event added");
                    Console.WriteLine();
                }

            }

        }

    }
}
