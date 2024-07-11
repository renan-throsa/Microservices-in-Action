using SpecialOffers.Domain.Models;

namespace SpecialOffers.Domain.Interfaces
{
    public interface IEventService
    {        
        Task<OperationResultModel> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
    }
}
