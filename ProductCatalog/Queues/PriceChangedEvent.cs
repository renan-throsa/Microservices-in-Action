namespace ProductCatalog.Queues
{
    public class PriceChangedEvent : Message
    {
        public static string MessageSubject = "events.price.changed";

        public override string Subject { get { return MessageSubject; } }

        public PriceChangeView Item { get; set; }

        public DateTime SavedAt { get; set; }

        public PriceChangedEvent(PriceChangeView item)
        {
            Item = item;
            SavedAt = DateTime.UtcNow;
        }
    }
}
