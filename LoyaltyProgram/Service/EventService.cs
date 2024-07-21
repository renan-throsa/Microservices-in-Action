using AutoMapper;
using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;
using LoyaltyProgram.Domain.Models;

namespace LoyaltyProgram.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly ILogger<ISpecialOffersClient> _logger;
        private readonly ISpecialOffersClient _specialOffersClient;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repository, ISpecialOffersClient specialOffersClient, ILogger<ISpecialOffersClient> logger, IMapper mapper)
        {
            _repository = repository;
            _specialOffersClient = specialOffersClient;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task FetchEvents()
        {
            _logger.LogInformation("Fetching latest events");
            
            IEnumerable<SpecialOfferViewModel> resp;

            try
            {
                resp = await _specialOffersClient.GetOffers();                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return;
            }

            if (resp.Any())
            {
                await _repository.AddEvents(_mapper.Map<IEnumerable<SpecialOffer>>(resp));
                _logger.LogInformation("Latest events updated");
            }
           
        }
    }
}
