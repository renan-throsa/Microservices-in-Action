using MongoDB.Bson;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddEvent(string eventName, ObjectId UserId, ObjectId ProductCatalogueId)
        {
            await _eventRepository.AddEvent(eventName, UserId, ProductCatalogueId);
        }

        public async Task<OperationResultModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var result = await _eventRepository.GetEvents(firstEventSequenceNumber, lastEventSequenceNumber);
            return Response(ResponseStatus.Ok, result);
        }

        private OperationResultModel Response(ResponseStatus valide, object? result = null)
        {
            return
            new OperationResultModel
            {
                Status = valide,
                Content = result
            };
        }
    }
}
