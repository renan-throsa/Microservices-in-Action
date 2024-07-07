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
               .ForMember(cart => cart.Id, x => x.MapFrom(y => y.ShoppingCartId == null ? ObjectId.Empty : new ObjectId(y.ShoppingCartId)))
               .ReverseMap();



            CreateMap<CartItemViewModel, CartItem>()
               .ForMember(cart => cart.ProductCatalogueId, x => x.MapFrom(y => y.ProductCatalogueId == null ? ObjectId.Empty : new ObjectId(y.ProductCatalogueId)))
               .ReverseMap();
        }
    }
}
