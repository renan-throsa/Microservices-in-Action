namespace LoyaltyProgram.Service
{
    public interface ISpecialOffersClient
    {
        Task ProcessEvents(Stream content);       
    }
}
