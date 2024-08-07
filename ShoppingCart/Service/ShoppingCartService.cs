﻿using AutoMapper;
using MongoDB.Bson;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System.Net;

namespace ShoppingCart.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductCatalogClient _productCatalogClient;
        private readonly IEventRepository _eventStore;
        private readonly IMapper _mapper;
        private readonly ILogger<IShoppingCartService> _logger;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductCatalogClient productCatalogClient, IEventRepository eventStore, IMapper mapper, ILogger<IShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productCatalogClient = productCatalogClient;
            _eventStore = eventStore;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<OperationResultModel> FindSync(string userId)
        {
            if (ObjectId.TryParse(userId, out var _))
            {
                var entity = await _shoppingCartRepository.FindSync(userId);
                if (entity == null)
                {
                    var message = $"Key:{userId} was not found.";
                    _logger.LogWarning(message);
                    return Response(HttpStatusCode.NotFound, message);
                }

                return Response(HttpStatusCode.Found, _mapper.Map<CartViewModel>(entity));
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {userId}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {userId}");
            }
        }

        public async Task<OperationResultModel> DeleteAsync(CartPostModel model)
        {
            var userId = model.UserId;

            if (!ObjectId.TryParse(model.UserId, out var _))
            {
                _logger.LogError($"Unable to parse the object: {model.UserId}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {model.UserId}");
            }

            var shoppingCart = await _shoppingCartRepository.FindSync(model.UserId);

            if (shoppingCart == null)
            {
                var message = $"Key:{userId} was not found.";
                _logger.LogWarning(message);
                return Response(HttpStatusCode.NotFound, message);
            }

            _logger.LogWarning($"Removing products {string.Format("[{0}]", string.Join(",", model.ProductIds))} from user's cart with id {model.UserId}");
            shoppingCart.RemoveItems(model.ProductIds);

            await _shoppingCartRepository.UpdateSync(shoppingCart);
            return Response(HttpStatusCode.OK, _mapper.Map<CartViewModel>(shoppingCart));

        }

        public async Task<OperationResultModel> SaveAsync(CartPostModel model)
        {
            if (!ObjectId.TryParse(model.UserId, out var _))
            {
                _logger.LogError($"Unable to parse the object: {model.UserId}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {model.UserId}");
            }

            _logger.LogInformation($"Adding products {string.Format("[{0}]", string.Join(",", model.ProductIds))} to user's cart with id {model.UserId}");
            var shoppingCartItemsTask = _productCatalogClient.Query(model.ProductIds);

            var shoppingCart = await _shoppingCartRepository.FindSync(model.UserId);

            IEnumerable<CartItem> shoppingCartItems;
            try
            {
                shoppingCartItems = _mapper.Map<IEnumerable<CartItem>>(await shoppingCartItemsTask);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return Response(HttpStatusCode.InternalServerError, ex.Message);
            }

            if (shoppingCart is null)
            {
                _logger.LogWarning($"Key:{model.UserId} was not found. Saving a new {typeof(Cart).FullName}");
                shoppingCart = new Cart(new ObjectId(model.UserId));
                await shoppingCart.AddItems(shoppingCartItems, _eventStore);
                await _shoppingCartRepository.AddSync(shoppingCart);
            }
            else
            {
                await shoppingCart.AddItems(shoppingCartItems, _eventStore);
                await _shoppingCartRepository.UpdateSync(shoppingCart);
            }

            var mapping = _mapper.Map<CartViewModel>(shoppingCart);
            return Response(HttpStatusCode.OK, mapping);
        }


        private OperationResultModel Response(HttpStatusCode valide, object? result = null)
        {
            return
            new OperationResultModel
            {
                Status = valide,
                Content = result
            };
        }

        public async Task UpdatedAsync(string productId)
        {
            var id = new ObjectId(productId);

            var productTask = _productCatalogClient.Find(productId);

            var carts = _shoppingCartRepository.FindBy(productId);

            if (!carts.Any())
            {
                _logger.LogWarning($"No carts to be updated");
            }

            var product = _mapper.Map<CartItem>(await productTask);

            _logger.LogWarning($"Updating all carts item with {product.ProductCatalogueId} | {product.ProductName}");

            foreach (var cart in carts)
            {
                var oldItem = cart.Items.First(i => i.ProductCatalogueId == id);
                cart.Items.RemoveWhere(i => i.ProductCatalogueId == id);
                cart.Items.Add(product with { Quantity = oldItem.Quantity });
                await _shoppingCartRepository.UpdateSync(cart);
            }

            _logger.LogWarning($"Total of {carts.Count()} carts updated");
        }
    }
}
