using MongoDB.Bson;

namespace ShoppingCart.Domain.Entities
{
    public record Event(ObjectId Id, ObjectId UserId, ObjectId ProductCatalogueId, long SequenceNumber, DateTime OccuredAt, string Name);

    /*
    public class Event
    {
        ObjectId EventId { get; set; }
        public long SequenceNumber { get; }
        public DateTimeOffset OccurredAt { get; }
        public string Name { get; }
        public object Content { get; }

        public Event(long sequenceNumber, DateTimeOffset occurredAt, string name, object content)
        {
            SequenceNumber = sequenceNumber;
            OccurredAt = occurredAt;
            Name = name;
            Content = content;
        }
    } 
     */


}
