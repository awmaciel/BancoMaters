# Justificativa da Implementação
Para este teste, eu segui uma abordagem utilizando um **grafo ponderado**, onde cada cidade é representada como um nó, e cada rota entre duas cidades é uma aresta com um custo associado.
No entanto, como o desafio especifica que o algoritmo de **Dijkstra** não deve ser utilizado, optei por uma solução alternativa. Em vez de usar **Dijkstra**, implementei uma busca exaustiva, onde todas as rotas possíveis entre a origem e o destino são exploradas. Após essa busca, um filtro é aplicado para selecionar a rota com o menor custo.
Essa abordagem garante que todas as rotas possíveis sejam analisadas, respeitando o requisito de não utilizar o algoritmo de Dijkstra e oferecendo a solução da melhor rota com o menor custo, mesmo que ela envolva mais conexões.

# Sobre arquitetura e design de softwares:
* 1 Utilizei uma arquitetura em camadas e separei as responsabilidades.
* 2 Utilizei alguns conceitos do **S.O.L.I.D** e **Clean Code** detalhados abaixo:
	 * 2.1 Princípio da Responsabilidade Única (SRP): A responsabilidade de armazenamento de rotas está separada da responsabilidade de calcular a melhor rota.
	 * 2.2 Princípio Aberto/Fechado (OCP): Podemos adicionar novos tipos de rotas é repositorios (por exemplo, banco de dados) sem modificar a classe RouteService.cs.
	 * 2.3 Princípio da Substituição de Liskov (LSP): Não utilizei.
	 * 2.4 Princípio da Segregação de Interface (ISP): Interfaces pequenas e específicas foram criadas para lidar com o armazenamento e o serviço de rotas e as classes não são obrigadas a implementar metodos que não vão utilizar.
	 * 2.5 Princípio da Inversão de Dependência (DIP): As dependências são injetadas, o que permite maior flexibilidade e testabilidade.
	 * 2.6 Não ha comentarios excessivos, codigos não utilizados e nomes de variaveis e funções estão claros sobre oque fazem.

## Conclusão
Acredito que da forma como esta implementado a aplicação está modular, testável e extensível, o que é fundamental em projetos de software maiores e mais complexos.
Novas funcionalidades como novos tipos de rotas, fontes de dados ou funcionalidades poderiam ser implementados sem causar impactos no que já estava funcionando, deixei classes de rotas extras para mostrar o principio aberto/fechado do SOLID
