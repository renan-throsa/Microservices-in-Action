using AutoMapper;
using MongoDB.Bson;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Domain.Models;
using System.Linq.Expressions;
using System.Net;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IProductService> _logger;

        public ProductService(IProductRepository repository, IMapper mapper, ILogger<IProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public OperationResultModel All()
        {            
            return Response(HttpStatusCode.OK, _mapper.ProjectTo<IEnumerable<ProductViewModel>>(_repository.GetQueryable()));
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
            if (ObjectId.TryParse(Id, out var _))
            {
                var entity = await _repository.FindSync(Id);
                if (entity == null)
                {
                    var message = $"Key:{Id} was not found.";
                    _logger.LogWarning(message);
                    return Response(HttpStatusCode.NotFound, message);
                }

                return Response(HttpStatusCode.Found, _mapper.Map<ProductViewModel>(entity));
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {Id}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {Id}");
            }
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
