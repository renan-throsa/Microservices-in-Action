using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IEventService
    {
        Task<ResponseModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Task AddEvent(string eventName, object content);
    }
}
