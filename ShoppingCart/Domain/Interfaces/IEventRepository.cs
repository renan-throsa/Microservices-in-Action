﻿using ShoppingCart.Domain.Entites;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Task AddEvent(string eventName, object content);
    }
}
