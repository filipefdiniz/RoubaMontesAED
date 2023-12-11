TRABALHO FINAL AED - ROUBA MONTES
Análise e Desenvolvimento de SIstemas| 2°Período | PUC MINAS - SÃO GABRIEL

GRUPO: Filipe Ferreira Diniz, Pedro Henrique Macena Rocha e Yuri Lucas Silva Ferreira.
DISCIPLINA: Algoritmos e Estruturas de Dados


PLANEJAMENTO DO FUNCIONAMENTO DO ALGORITMO:
MENU INICIAL
1- Iniciar um novo jogo
2 - Ver histórico

MENU DE HISTÓRICO
1 - Ver histórico de um jogador em específico ( últimas 5 partidas)

INíCIO DO JOGO
1 - Inserir quantidade de jogadores
2 - Cadastro de jogadores
3 - Inserir quantidade de baralhos

INÍCIO DA PARTIDA
LOOP(Enquanto o baralho não acabar)
{

JOGAR
1 - Mostrar o nome do jogador da vez
2 - Exibir uma carta do monte de compras (Carta da vez)
3 - Exibir cartas da mesa (Área de descarte)
4 - Exibir cartas de cima de cada jogador (Nome do jogador, carta do topo, quantidade de cartas)

OPÇÕES DE AÇÃO
1 - Roubar monte de um jogador
2 - Roubar da mesa
3 - Descartar uma carta (caso não tenha carta igual)
}

FIM DO JOGO (monte de compras acabou)
1 - Mostrar o vencedor (Cartas acumuladas ordenadas, quantidade, nome do jogador) 
2 - Mostrar o ranking ( Nome do jogador e posição nas últimas partidas)

LOGS
1 - Arquivo de logs de ações dos jogadores e das partidas
2 - Arquivo de histórico de partidas


ESTRUTURAS UTILIZADAS:

Baralho: Pilha (Stack<Carta>)
Monte dos jogadores: Pilha (Stack<Carta>)
Área de Descarte: Lista (List<Carta>)
Carta da vez: Carta (Carta<valor, naipe>)
Jogadores: Fila (Queue<Jogador>)
Ranking Final: Vetor( Jogador[])


TECNOLOGIAS UTILIZADAS:

Visual Studio
.NET C#
Collections .NET
Git e Github
