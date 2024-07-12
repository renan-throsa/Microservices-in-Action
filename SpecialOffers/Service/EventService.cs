using AutoMapper;
using SpecialOffers.Domain;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;
using System.Net;

namespace SpecialOffers.Service
{
    public class EventService : IEventService
    {
        private readonly ISpecialOfferRepository _eventRepository;
        private readonly ILogger<IEventService> _logger;

        private readonly IMapper _mapper;
        public EventService(ISpecialOfferRepository eventRepository, IMapper mapper, ILogger<IEventService> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResultModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            _logger.LogInformation($"Fetching offers from {firstEventSequenceNumber} to {lastEventSequenceNumber}");
            var result = await _eventRepository.GetOffers(firstEventSequenceNumber, lastEventSequenceNumber);
            return Response(HttpStatusCode.OK, _mapper.Map<IEnumerable<SpecialOfferViewModel>>(result));
        }

        private OperationResultModel Response(HttpStatusCode status, object? result = null)
        {
            return
            new OperationResultModel
            {
                Status = status,
                Content = result
            };
        }

    }
}
