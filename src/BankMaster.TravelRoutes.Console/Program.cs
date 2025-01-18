using BankMaster.TravelRoutes.BLL;
using BankMaster.TravelRoutes.BLL.Interfaces;
using BankMaster.TravelRoutes.BLL.Model;
using BankMaster.TravelRoutes.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var rotaService = host.Services.GetRequiredService<IRouteServiceStrategy>();
        var routeRepository = host.Services.GetRequiredService<IRouteRepository>();

        var pontos = new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Pontos disponiveis para montagem de origem e destino:");
            foreach (var ponto in pontos)
                Console.WriteLine($"- {ponto}");

            Console.WriteLine("\nDigite a rota desejada (ex: GRU-CDG) ou pressione ESC para sair:");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Entrada inválida. Por favor, forneça uma rota válida.");
                continue;
            }

            var partes = input.Split('-');

            if (partes.Length == 2)
            {
                string origem = partes[0].Trim().ToUpper();
                string destino = partes[1].Trim().ToUpper();

                if (pontos.Contains(origem) && pontos.Contains(destino))
                {
                    Console.WriteLine("\nEscolha a estratégia de rota:");
                    Console.WriteLine("1 - Mais barata");
                    Console.WriteLine("2 - Mais curta");
                    Console.WriteLine("3 - Mais longa");

                    string escolha = Console.ReadLine();

                    IRouteStrategy estrategiaEscolhida = escolha switch
                    {
                        "1" => new CheapestRouteCalculator(),
                        "2" => new ShortestRouteCalculator(),
                        _ => new CheapestRouteCalculator()
                    };

                    rotaService.SetRouteStrategy(estrategiaEscolhida);
                    var resultado = await rotaService.FindBestRouteAsync(origem, destino);
                    Console.WriteLine($"\nMelhor Rota: {resultado.route} ao custo de ${resultado.cost}");

                    Console.WriteLine("Deseja adicionar uma nova rota? (s/n)");
                    var resposta = Console.ReadLine().ToLower();
                    if (resposta == "s")
                    {
                        Console.WriteLine("Digite a origem, destino e custo da nova rota (ex: GRU-BRC-10):");
                        var novaRota = Console.ReadLine();
                        if (!string.IsNullOrEmpty(novaRota))
                        {
                            var partesNovaRota = novaRota.Split('-');
                            if (partesNovaRota.Length == 3)
                            {
                                string novaOrigem = partesNovaRota[0].Trim().ToUpper();
                                string novoDestino = partesNovaRota[1].Trim().ToUpper();
                                int custo = int.Parse(partesNovaRota[2].Trim());

                                var rota = new Route(novaOrigem, novoDestino, custo);
                                routeRepository.AddRoute(rota);
                                Console.WriteLine("Nova rota adicionada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Formato de rota inválido.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nOrigem ou destino inválido. Por favor, escolha entre os pontos disponíveis.");
                }
            }
            else
                Console.WriteLine("Formato de entrada inválido.");

            Console.WriteLine("\nPressione Barra de Espaço para continuar ou ESC para sair.");
            var tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.Escape)
                break;
            else if (tecla != ConsoleKey.Spacebar)
            {
                Console.WriteLine("Tecla inválida. Pressione Barra de Espaço para continuar ou ESC para sair.");
                continue;
            }
        }

        Console.WriteLine("\nObrigado por usar o sistema de Rotas!");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<IRouteServiceStrategy, RouteServiceStrategy>();
                services.AddScoped<IRouteRepository, RouteRepository>();
                services.AddScoped<IRouteStrategy, CheapestRouteCalculator>();
            });
}

