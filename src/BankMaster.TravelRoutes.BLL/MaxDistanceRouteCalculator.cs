using BankMaster.TravelRoutes.BLL.Interfaces;

namespace BankMaster.TravelRoutes.BLL
{
    public class MaxDistanceRouteCalculator : IRouteStrategy
    {
        public Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination, IRouteRepository routeRepository)
        {
            return Task.FromResult(($"Método de maior distância não implementado para {origin} -> {destination}", 0));
        }
    }
}
