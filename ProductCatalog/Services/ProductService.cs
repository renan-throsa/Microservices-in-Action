using AutoMapper;
using MongoDB.Bson;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Domain.Models;
using ProductCatalog.Queues;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IProductService> _logger;
        private readonly IQueue _queue;

        public ProductService(IProductRepository repository, IQueue queue, IMapper mapper, ILogger<IProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _queue = queue;
        }

        public OperationResultModel All()
        {
            return Response(HttpStatusCode.OK, _mapper.ProjectTo<ProductViewModel>(_repository.GetQueryable()));
        }

        public Task<OperationResultModel> FindAsync(Expression<Func<ProductViewModel, bool>> filter)
        {
            throw new NotImplementedException();
        }


        public async Task<OperationResultModel> FindSync(ObjectId key)
        {
            var entity = await _repository.FindSync(key);
            if (entity == null)
            {
                var message = $"Key:{key} not found.";
                _logger.LogWarning(message);
                return Response(HttpStatusCode.NotFound);
            }

            return Response(HttpStatusCode.Found, _mapper.Map<ProductViewModel>(entity));
        }

        public async Task<OperationResultModel> FindSync(string Id)
        {
            var resolve = async delegate (Product entity)
            {
                return Response(HttpStatusCode.Found, _mapper.Map<ProductViewModel>(entity));
            };

            return await TryFindSync(Id, resolve);

        }

        public async Task<OperationResultModel> FindSync(string[] Ids)
        {
            Ids = Ids.Distinct().ToArray();
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

            return Response(HttpStatusCode.OK, result);

        }

        public async Task<OperationResultModel> UpdateSync(ProductPatchModel model)
        {
            var resolve = async delegate (Product entity)
            {
                var toUpdate = entity with { Name = model.Name, Description = model.Description, Price = model.Price };

                await _repository.UpdateAsync(toUpdate);

                var productChanged = new ProductChangedEvent(model.Id);
                await _queue.Publish(productChanged.Subject, JsonSerializer.Serialize(productChanged));
                _logger.LogInformation($"Change sent: {JsonSerializer.Serialize(productChanged)}");

                return Response(HttpStatusCode.OK, _mapper.Map<ProductViewModel>(toUpdate));
            };

            return await TryFindSync(model.Id, resolve);

        }


        private async Task<OperationResultModel> TryFindSync(string id, Func<Product, Task<OperationResultModel>> resolve)
        {
            if (ObjectId.TryParse(id, out var _))
            {
                var entity = await _repository.FindSync(id);
                if (entity == null)
                {
                    var message = $"Key:{id} was not found.";
                    _logger.LogWarning(message);
                    return Response(HttpStatusCode.NotFound, message);
                }
                return await resolve(entity);
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {id}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {id}");
            }

        }

        private OperationResultModel Response(HttpStatusCode status, object? result = null)
        {
            return
            new OperationResultModel
            {
                Status = status,
                Content = result
            };
        }
    }
}
