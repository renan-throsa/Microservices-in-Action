using AutoMapper;
using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Models;
using MongoDB.Bson;

namespace LoyaltyProgram.Utils
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SpecialOfferViewModel, SpecialOffer>()
               .ForMember(entity => entity.Id, x => x.MapFrom(y => y.Id == null ? ObjectId.Empty : new ObjectId(y.Id)))
               .ForMember(entity => entity.ProductsIds, x => x.MapFrom(y => new HashSet<string>(y.productsIds)))
               .ReverseMap();
  
        }
    }
}
