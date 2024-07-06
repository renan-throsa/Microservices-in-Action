﻿using AutoMapper;
using MongoDB.Bson;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IProductRepository> _logger;

        public ProductService(IProductRepository repository, IMapper mapper, ILogger<IProductRepository> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public Response All()
        {
            _logger.LogInformation($"Listing {typeof(ProductViewModel).FullName}");
            return Response(ResponseStatus.Ok, _mapper.ProjectTo<ProductViewModel>(_repository.GetQueryable()));
        }

        public Task<Response> FindAsync(Expression<Func<ProductViewModel, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> FindSync(ObjectId key)
        {
            _logger.LogInformation($"Searching by key:{key}");
            var entity = await _repository.FindSync(key);
            if (entity == null)
            {
                var message = $"Key:{key} not found.";
                _logger.LogWarning(message);
                return Response(ResponseStatus.NotFound);
            }

            return Response(ResponseStatus.Found, _mapper.Map<ProductViewModel>(entity));
        }

        public async Task<Response> FindSync(string Id)
        {
            _logger.LogInformation($"Searching by key:{Id}");
            if (ObjectId.TryParse(Id, out var _))
            {
                var entity = await _repository.FindSync(Id);
                if (entity == null)
                {
                    var message = $"Key:{Id} was not found.";
                    _logger.LogWarning(message);
                    return Response(ResponseStatus.NotFound, message);
                }

                return Response(ResponseStatus.Found, _mapper.Map<ProductViewModel>(entity));
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {Id}");
                return Response(ResponseStatus.BadRequest, $"Unable to parse the object: {Id}");
            }
        }

        public async Task<Response> FindSync(string[] Ids)
        {
            _logger.LogInformation($"Searching by key:{Ids}");
            var result = new List<ProductViewModel>();
            foreach (var Id in Ids)
            {
                if (ObjectId.TryParse(Id, out var _))
                {
                    var entity = await _repository.FindSync(Id);
                    if (entity == null)
                    {
                        _logger.LogWarning($"Key:{Id} was not found.");
                        continue;
                    }
                    result.Add(_mapper.Map<ProductViewModel>(entity));
                }
                else
                {
                    _logger.LogError($"Unable to parse the object: {Id}");
                }
            }

            return Response(ResponseStatus.Ok, result);

        }

        private Response Response(ResponseStatus valide, object? result = null)
        {
            return
            new Response
            {
                Status = valide,
                Payload = result
            };
        }
    }
}