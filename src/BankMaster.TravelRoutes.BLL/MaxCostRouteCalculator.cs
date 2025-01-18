using BankMaster.TravelRoutes.BLL.Interfaces;

namespace BankMaster.TravelRoutes.BLL
{
    public class MaxCostRouteCalculator : IRouteStrategy
    {
        public Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination, IRouteRepository routeRepository)
        {
            return Task.FromResult(($"Método de maior custo não implementado para {origin} -> {destination}", 0));
        }
    }
}
