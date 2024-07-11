
namespace SpecialOffers.Domain.Models
{
    public class SpecialOfferViewModel
    {
        public string Id { get; set; }
        public long SequenceNumber { get; set; }
        public DateTime OccuredAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OldOfferId { get; set; }
    }
}
