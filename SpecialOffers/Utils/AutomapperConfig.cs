using AutoMapper;
using MongoDB.Bson;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Models;

namespace SpecialOffers.Utils
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SpecialOfferViewModel, SpecialOffer>()
               .ForMember(cart => cart.Id, x => x.MapFrom(y => y.Id == null ? ObjectId.Empty : new ObjectId(y.Id)))
               .ReverseMap();


        }
    }
}
