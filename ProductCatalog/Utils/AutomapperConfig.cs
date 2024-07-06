using AutoMapper;
using MongoDB.Bson;
using ProductCatalog.Domain;
using ProductCatalog.Services;

namespace ProductCatalog.Utils
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ProductViewModel, Product>()
                .ForMember(member => member.Id, x => x.MapFrom(y => y.Id == null ? ObjectId.Empty : new ObjectId(y.Id)))
                .ReverseMap();
        }
    }
}
