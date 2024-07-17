using PriceCalculation.Domain.Models;

namespace PriceCalculation.Domain.Interfaces
{
    public interface IPriceCalculationService
    {
        Task<OperationResultModel> CarryOut(PriceCalculationPostModel entity);
    }
}
