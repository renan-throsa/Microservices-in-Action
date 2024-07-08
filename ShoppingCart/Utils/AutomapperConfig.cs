using AutoMapper;
using MongoDB.Bson;
using ShoppingCart.Domain.Entites;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Utils
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<CartViewModel, Cart>()
               .ForMember(cart => cart.Id, x => x.MapFrom(y => y.Id == null ? ObjectId.Empty : new ObjectId(y.Id)))
               .ForMember(cart => cart.UserId, x => x.MapFrom(y => y.UserId == null ? ObjectId.Empty : new ObjectId(y.UserId)))
               .ReverseMap();

            CreateMap<CartItemViewModel, CartItem>().ReverseMap();
        }
    }
}
