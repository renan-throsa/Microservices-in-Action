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

        public async Task AddEvent(string eventName, object content)
        {
            await _eventRepository.AddEvent(eventName, content);
        }

        public async Task<ResponseModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var result = await _eventRepository.GetEvents(firstEventSequenceNumber, lastEventSequenceNumber);
            return Response(ResponseStatus.Ok, result);
        }

        private ResponseModel Response(ResponseStatus valide, object? result = null)
        {
            return
            new ResponseModel
            {
                Status = valide,
                Payload = result
            };
        }
    }
}
