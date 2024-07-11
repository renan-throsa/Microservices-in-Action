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
        private readonly IMapper _mapper;
        public EventService(ISpecialOfferRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<OperationResultModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
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
