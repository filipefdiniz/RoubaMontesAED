using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        ImprimirTituloJogo();
        

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("==== LET'S PLAY! ====");
        Console.ResetColor();
        Console.WriteLine("1 - INICIAR UM JOGO");
        Console.WriteLine("2 - VER HISTRÓRICO");
        Console.WriteLine("======================");
        Console.Write("Digite a opção desejada: "); 
        int opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                //Instanciar novo jogo
                Jogo Jogo = CriarJogo();
                //Iniciar o jogo depois de ter sido instanciado
                Jogo.Jogar();
                break;
            case 2:
                break;
            default:
                break;
        }
        




        //Método para criar um novo jogo - Jogadores - Distribuição de cartas - Monte de compras
        static Jogo CriarJogo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("=== INSIRA OS DADOS PARA COMEÇAR A PARTIDA ===");
            Console.ResetColor();
            Console.Write("Quantidade de jogadores da partida: ");
            int quantidadeJogadores = int.Parse(Console.ReadLine());

            //Fila da ordem de jogadores
            Queue<Jogador> JogadoresDaPartida = new Queue<Jogador>(); // Criando fila

            for (int i = 1; i <= quantidadeJogadores; i++)
            {
                Console.Write($"Insira o nome do {i}° Jogador: ");
                string nomeJogador = Console.ReadLine();

                Jogador jogador = new Jogador(i, nomeJogador);
                JogadoresDaPartida.Enqueue(jogador);
            }

            //Inserir quantidade de baralhos
            Console.Write("Quantidade de baralhos para a partida: ");
            int quantidadeBaralhos = int.Parse(Console.ReadLine());

            //Criar um baralho 
            Baralho baralho = new Baralho();
            List<Carta> lista = baralho.CriarBaralho(quantidadeBaralhos);

            //Inserir as cartas do baralho no monte de compras
            Stack<Carta> monte = new Stack<Carta>();
            foreach (Carta carta in lista)
            {
                monte.Push(carta);
            }

            //Limpar a lista de cartas (as cartas estão inseridas na pilha)
            baralho.baralho.Clear();

            //Inserir 4 cartas iniciais na area de descarte
            List<Carta> area = new List<Carta>();
            for (int i = 0; i < 4; i++)
            {
                area.Add(monte.Pop());
            }
            Carta primeiraCarta = monte.Pop();
            //Instanciar um novo jogo
            Jogo novoJogo = new Jogo(JogadoresDaPartida, baralho, monte, area, primeiraCarta);
            
            Console.Clear();
            novoJogo.Imprimir();

            return novoJogo;

        }

        static void ImprimirTituloJogo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
██████╗░░█████╗░██╗░░░██╗██████╗░░█████╗░  ███╗░░░███╗░█████╗░███╗░░██╗████████╗███████╗░██████╗
██╔══██╗██╔══██╗██║░░░██║██╔══██╗██╔══██╗  ████╗░████║██╔══██╗████╗░██║╚══██╔══╝██╔════╝██╔════╝
██████╔╝██║░░██║██║░░░██║██████╦╝███████║  ██╔████╔██║██║░░██║██╔██╗██║░░░██║░░░█████╗░░╚█████╗░
██╔══██╗██║░░██║██║░░░██║██╔══██╗██╔══██║  ██║╚██╔╝██║██║░░██║██║╚████║░░░██║░░░██╔══╝░░░╚═══██╗
██║░░██║╚█████╔╝╚██████╔╝██████╦╝██║░░██║  ██║░╚═╝░██║╚█████╔╝██║░╚███║░░░██║░░░███████╗██████╔╝
╚═╝░░╚═╝░╚════╝░░╚═════╝░╚═════╝░╚═╝░░╚═╝  ╚═╝░░░░░╚═╝░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░╚══════╝╚═════╝░");
            Console.ResetColor();
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("======================================= | \u2665 | \u2666 | \u2663 | \u2660 | =======================================");
            
            
            Console.WriteLine();
        }


    }
    
}