using BankMaster.TravelRoutes.BLL.Model;

namespace BankMaster.TravelRoutes.BLL.Interfaces
{
    public interface IRouteRepository
    {
        Task<List<Route>> SearchRoutesByOriginAsync(string origin);
        void AddRoute(Route route);
    }
}
