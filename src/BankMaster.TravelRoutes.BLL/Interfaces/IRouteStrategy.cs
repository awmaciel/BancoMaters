namespace BankMaster.TravelRoutes.BLL.Interfaces
{
    public interface IRouteStrategy
    {
        Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination, IRouteRepository routeRepository);
    }
}
