using LoyaltyProgram.Domain.Interfaces;
using Quartz;

namespace LoyaltyProgram.Jobs
{
    [DisallowConcurrentExecution]
    public class SpecialOffersJob : IJob
    {
        private readonly IEventService _eventService;

        public SpecialOffersJob(IEventService service)
        {
            _eventService = service;

        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _eventService.FetchEvents();
        }

    }
}
