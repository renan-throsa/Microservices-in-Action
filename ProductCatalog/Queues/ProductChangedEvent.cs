﻿namespace ProductCatalog.Queues
{
    public class ProductChangedEvent : Message
    {
        private const string MessageSubject = "events.product.changed";

        public override string Subject { get { return MessageSubject; } }

        public string ProductId { get; set; }

        public DateTime SavedAt { get; set; }

        public ProductChangedEvent(string productId)
        {
            ProductId = productId;
            SavedAt = DateTime.UtcNow;
        }
    }
}
