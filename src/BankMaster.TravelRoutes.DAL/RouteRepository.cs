using BankMaster.TravelRoutes.BLL.Interfaces;
using BankMaster.TravelRoutes.BLL.Model;

namespace BankMaster.TravelRoutes.DAL
{
    public class RouteRepository : IRouteRepository
    {
        private readonly List<Route> _routes;
        private readonly string _filePath = $@"{Directory.GetCurrentDirectory()}\rotas.txt";

        public RouteRepository()
        {
            _routes = LoadRoutesFromFile();
        }

        private List<Route> LoadRoutesFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"Arquivo {_filePath} não encontrado.");
                return new List<Route>();
            }


            var lines = File.ReadAllLines(_filePath);
            Console.WriteLine($"Linhas carregadas do arquivo: {lines.Length}");

            var routes = lines.Select(line =>
            {
                var parts = line.Split(',');
                var route = new Route(parts[0].Trim(), parts[1].Trim(), int.Parse(parts[2].Trim()));
                Console.WriteLine($"Rota carregada: {route.Origin} -> {route.Destination} com custo {route.Cost}");
                return route;
            }).ToList();

            return routes;
        }

        public Task<List<Route>> SearchRoutesByOriginAsync(string origem)
        {
            var rotas = _routes.Where(r => r.Origin == origem).ToList();
            return Task.FromResult(rotas);
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
            SaveRoutesToFile();
        }

        private void SaveRoutesToFile()
        {
            var lines = _routes.Select(r => $"{r.Origin},{r.Destination},{r.Cost}");
            File.WriteAllLines(_filePath, lines);
        }
    }
}



