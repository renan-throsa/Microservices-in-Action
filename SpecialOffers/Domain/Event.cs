﻿namespace SpecialOffers.Domain
{
    public record Event(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content);
}
