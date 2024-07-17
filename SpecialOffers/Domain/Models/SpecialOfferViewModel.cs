
namespace SpecialOffers.Domain.Models
{
    public class SpecialOfferViewModel
    {
        public string Id { get; set; }
        public DateTime OccuredAt { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] ProductsIds { get; set; }
        public float Discount { get; set; }
    }
}
