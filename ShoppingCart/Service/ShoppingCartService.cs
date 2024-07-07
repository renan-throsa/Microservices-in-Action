using AutoMapper;
using MongoDB.Bson;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductCatalogClient _productCatalogClient;
        private readonly IEventRepository _eventStore;
        private readonly IMapper _mapper;
        private readonly ILogger<IShoppingCartService> _logger;

        public ShoppingCartService(IShoppingCartRepository shoppingCartStore, IProductCatalogClient productCatalogClient, IEventRepository eventStore, IMapper mapper, ILogger<IShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartStore;
            _productCatalogClient = productCatalogClient;
            _eventStore = eventStore;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<ResponseModel> FindSync(string userId)
        {
            _logger.LogInformation($"Fetching {typeof(CartViewModel).FullName} whit id {userId}");
            if (ObjectId.TryParse(userId, out var _))
            {
                var entity = await _shoppingCartRepository.FindSync(userId);
                if (entity == null)
                {
                    var message = $"Key:{userId} was not found.";
                    _logger.LogWarning(message);
                    return Response(ResponseStatus.NotFound, message);
                }

                return Response(ResponseStatus.Found, _mapper.Map<CartViewModel>(entity));
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {userId}");
                return Response(ResponseStatus.BadRequest, $"Unable to parse the object: {userId}");
            }
        }

        public async Task<ResponseModel> DeleteAsync(CartPostModel model)
        {
            _logger.LogInformation($"Removing products {string.Format("[{0}]", string.Join(",", model.ProductIds))} from user's cart with id {model.UserId}");
            var shoppingCart = await _shoppingCartRepository.FindSync(model.UserId);
            shoppingCart.RemoveItems(model.ProductIds, _eventStore);
            await _shoppingCartRepository.AddSync(shoppingCart);
            return Response(ResponseStatus.Ok, _mapper.Map<CartViewModel>(shoppingCart));
        }

        public async Task<ResponseModel> AddAsync(CartPostModel model)
        {
            _logger.LogInformation($"Adding products {string.Format("[{0}]", string.Join(",", model.ProductIds))} to user's cart with id {model.UserId}");
            var shoppingCart = await _shoppingCartRepository.FindSync(model.UserId);
            var shoppingCartItems = await _productCatalogClient.GetShoppingCartItems(model.ProductIds);            
            shoppingCart.AddItems(shoppingCartItems, _eventStore);
            await _shoppingCartRepository.UpdateSync(shoppingCart);
            return Response(ResponseStatus.Ok, _mapper.Map<CartViewModel>(shoppingCart));
        }


        private ResponseModel Response(ResponseStatus valide, object? result = null)
        {
            return
            new ResponseModel
            {
                Status = valide,
                Payload = result
            };
        }
    }
}
