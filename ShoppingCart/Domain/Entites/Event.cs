using MongoDB.Bson;

namespace ShoppingCart.Domain.Entites
{
    public record Event(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content, ObjectId? EventId = null);

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
