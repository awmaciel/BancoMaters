using BankMaster.TravelRoutes.BLL.Interfaces;

public class RouteServiceStrategy : IRouteServiceStrategy
{
    private readonly IRouteRepository _routeRepository;
    private readonly Dictionary<string, (string route, int cost)> _cachedRoutes;
    private IRouteStrategy _routeStrategy;

    public RouteServiceStrategy(IRouteRepository routeRepository, IRouteStrategy routeStrategy)
    {
        _routeRepository = routeRepository;
        _cachedRoutes = new Dictionary<string, (string route, int cost)>();
        _routeStrategy = routeStrategy;
    }

    public void SetRouteStrategy(IRouteStrategy routeStrategy)
    {
        _routeStrategy = routeStrategy;
    }

    public async Task<(string route, int cost)> FindBestRouteAsync(string origin, string destination)
    {
        string key = $"{origin}-{destination}";

        if (_cachedRoutes.ContainsKey(key))
        {
            Console.WriteLine($"Rota encontrada no cache: {key}");
            return _cachedRoutes[key];
        }

        var bestRoute = await _routeStrategy.FindBestRouteAsync(origin, destination, _routeRepository);

        _cachedRoutes[key] = bestRoute;

        return bestRoute;
    }
}
