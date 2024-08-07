﻿using AutoMapper;
using MongoDB.Bson;
using SpecialOffers.Domain;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;
using System.Linq.Expressions;
using System.Net;

namespace SpecialOffers.Service
{
    public class SpecialOfferService : ISpecialOfferService
    {

        private readonly ISpecialOfferRepository _specialOfferRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ISpecialOfferService> _logger;

        public SpecialOfferService(ISpecialOfferRepository specialOfferRepository, IMapper mapper, ILogger<ISpecialOfferService> logger)
        {
            _specialOfferRepository = specialOfferRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public Task<OperationResultModel> AddOffer(SpecialOfferPostModel offer)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> FindAsync(Expression<Func<SpecialOfferViewModel, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> FindSync(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultModel> FindOffers(HashSet<string> productIds)
        {
            IEnumerable<SpecialOffer> query = await _specialOfferRepository.FindOffers(productIds);

            if (!query.Any()) return Response(HttpStatusCode.NoContent);

            List<SpecialOffer> result = new();

            foreach (var item in query)
            {
                if (item.ProductsIds.Intersect(productIds).Any())
                {
                    result.Add(item with { ProductsIds = new HashSet<string>(item.ProductsIds.Intersect(productIds)) });                    
                }
            }

            return Response(HttpStatusCode.OK, _mapper.Map<IEnumerable<SpecialOfferViewModel>>(result));

            /*
            var query = _specialOfferRepository.GetQueryable().Where(so => so.DueDate < DateTime.Now);

            if (!query.Any()) return Response(HttpStatusCode.NoContent);

            query = query.Where(so => so.ProductsIds.Intersect(productIds).Any());

            if (!query.Any()) return Response(HttpStatusCode.NoContent);

            return Response(HttpStatusCode.OK, query.ToList());

            */
        }

        public async Task<OperationResultModel> FindOffers()
        {
            var query = await _specialOfferRepository.FindOffers();

            if (!query.Any()) return Response(HttpStatusCode.NoContent);

            return Response(HttpStatusCode.OK, _mapper.Map<IEnumerable<SpecialOfferViewModel>>(query));

        }

        public async Task<OperationResultModel> FindSync(string Id)
        {
            _logger.LogInformation($"Fetching {typeof(SpecialOfferViewModel).FullName} whit id {Id}");
            if (ObjectId.TryParse(Id, out var _))
            {
                var entity = await _specialOfferRepository.FindSync(Id);
                if (entity == null)
                {
                    var message = $"Key:{Id} was not found.";
                    _logger.LogWarning(message);
                    return Response(HttpStatusCode.NotFound, message);
                }

                return Response(HttpStatusCode.Found, _mapper.Map<SpecialOfferViewModel>(entity));
            }
            else
            {
                _logger.LogError($"Unable to parse the object: {Id}");
                return Response(HttpStatusCode.BadRequest, $"Unable to parse the object: {Id}");
            }
        }

        public Task<OperationResultModel> UpdateOffer(SpecialOfferPutModel offer)
        {
            throw new NotImplementedException();
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
