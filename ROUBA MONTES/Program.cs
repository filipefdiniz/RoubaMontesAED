using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        //Instanciar novo jogo
        Jogo Jogo = CriarJogo();

        Jogo.Jogar();




        //Método para criar um novo jogo - Jogadores - Distribuição de cartas - Monte de compras
        static Jogo CriarJogo()
        {
            Console.Write("Insira a quantidade de joagdores: ");
            int quantidadeJogadores = int.Parse(Console.ReadLine());

            //Fila da ordem de jogadores
            Queue<Jogador> JogadoresDaPartida = new Queue<Jogador>(); // Criando fila

            for (int i = 1; i <= quantidadeJogadores; i++)
            {
                Console.Write("Insira o nome do jogador: ");
                string nomeJogador = Console.ReadLine();

                Jogador jogador = new Jogador(i, nomeJogador);
                JogadoresDaPartida.Enqueue(jogador);
            }

            //Inserir quantidade de baralhos
            Console.Write("Insira a quantidade de baralhos que deseja utilizar na partida: ");
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

            baralho.Imprimir();
            novoJogo.Imprimir();

            return novoJogo;

        }

    }
    
}