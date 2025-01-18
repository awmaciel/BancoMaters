namespace BankMaster.TravelRoutes.BLL.Interfaces
{
    public interface IRouteServiceStrategy
    {
        Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination);
        void SetRouteStrategy(IRouteStrategy routeStrategy);
    }
}
