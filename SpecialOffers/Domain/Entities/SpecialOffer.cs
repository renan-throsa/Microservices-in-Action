﻿using MongoDB.Bson;

namespace SpecialOffers.Domain.Entities
{
    public record SpecialOffer(
        ObjectId Id, 
        long SequenceNumber, 
        DateTime OccuredAt, 
        string Name, 
        string Description, 
        ObjectId? OldOfferId = null);

}