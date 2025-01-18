using BankMaster.TravelRoutes.BLL;
using BankMaster.TravelRoutes.BLL.Interfaces;
using BankMaster.TravelRoutes.BLL.Model;
using Moq;

namespace BankMaster.TravelRoutes.TEST
{
    [TestClass]
    public class RouteServiceTests
    {
        private Mock<IRouteRepository> _mockRouteRepository;
        private RouteServiceStrategy _routeService;

        [TestInitialize]
        public void Setup()
        {
            _mockRouteRepository = new Mock<IRouteRepository>();

            _routeService = new RouteServiceStrategy(_mockRouteRepository.Object, new CheapestRouteCalculator());
        }

        [TestMethod]
        public async Task TesteEncontrarMelhorRota()
        {
            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("GRU"))
                .ReturnsAsync(new List<Route>
                {
                    new Route("GRU", "BRC", 10),
                    new Route("GRU", "CDG", 75),
                    new Route("GRU", "SCL", 20),
                    new Route("GRU", "ORL", 56)
                });

            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("BRC"))
                .ReturnsAsync(new List<Route>
                {
                    new Route("BRC", "SCL", 5)
                });

            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("SCL"))
                .ReturnsAsync(new List<Route>
                {
                    new Route("SCL", "ORL", 20)
                });

            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("ORL"))
                .ReturnsAsync(new List<Route>
                {
                    new Route("ORL", "CDG", 5)
                });

            var resultado = await _routeService.FindBestRouteAsync("GRU", "CDG");

            Assert.AreEqual("GRU - BRC - SCL - ORL - CDG", resultado.route);
            Assert.AreEqual(40, resultado.cost);
        }

        [TestMethod]
        public async Task TesteRotaNaoEncontrada()
        {
            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("GRU"))
                .ReturnsAsync(new List<Route>());

            var resultado = await _routeService.FindBestRouteAsync("GRU", "CDG");

            Assert.AreEqual("Nenhuma rota encontrada", resultado.route);
            Assert.AreEqual(0, resultado.cost);
        }

        [TestMethod]
        public async Task TesteRotaDireta()
        {
            _mockRouteRepository.Setup(repo => repo.SearchRoutesByOriginAsync("GRU"))
                .ReturnsAsync(new List<Route>
                {
                    new Route("GRU", "CDG", 75)
                });

            var resultado = await _routeService.FindBestRouteAsync("GRU", "CDG");

            Assert.AreEqual("GRU - CDG", resultado.route);
            Assert.AreEqual(75, resultado.cost);
        }
    }
}



