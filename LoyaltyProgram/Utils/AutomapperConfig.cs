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
               .ForMember(cart => cart.Id, x => x.MapFrom(y => y.Id == null ? ObjectId.Empty : new ObjectId(y.Id)))               
               .ReverseMap();
            
        }
    }
}
