using BankMaster.TravelRoutes.BLL.Interfaces;

namespace BankMaster.TravelRoutes.BLL
{
    public class CheapestRouteCalculator : IRouteStrategy
    {
        public async Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination, IRouteRepository routeRepository)
        {
            var paths = await SearchAllRoutesAsync(origin, destination, new List<string>(), 0, routeRepository);

            if (paths.Count == 0)
                return ("Nenhuma rota encontrada", 0);

            var bestRoute = paths.OrderBy(c => c.Cost).FirstOrDefault();
            return bestRoute;
        }

        private async Task<List<(string Route, int Cost)>> SearchAllRoutesAsync(string origin, string destination, List<string> currentPath, int currentCost, IRouteRepository routeRepository)
        {
            List<(string Route, int Cost)> paths = new List<(string, int)>();

            currentPath.Add(origin);

            if (origin == destination)
                paths.Add((string.Join(" - ", currentPath), currentCost));
            else
            {
                var possibleRoutes = await routeRepository.SearchRoutesByOriginAsync(origin);

                var filteredRoutes = possibleRoutes.Where(r => !currentPath.Contains(r.Destination));

                foreach (var route in filteredRoutes)
                {
                    var novosCaminhos = await SearchAllRoutesAsync(route.Destination, destination, new List<string>(currentPath), currentCost + route.Cost, routeRepository);
                    paths.AddRange(novosCaminhos);
                }
            }
            return paths;
        }
    }
}
