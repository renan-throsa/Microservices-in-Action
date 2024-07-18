using ClientGateway.Domain.Models;

namespace ClientGateway.Domain.Interfaces
{
    public interface IPriceCalculatorClient
    {
        Task<PriceCalculationViewModel> CarryOut(PriceCalculationPostModel model);
    }
}
