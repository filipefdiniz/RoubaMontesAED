using System;
using System.Collections.Generic;

class Jogo
{
    Queue<Jogador> Jogadores;
    Baralho Baralho;
    Stack<Carta> MonteDeCompras;
    List<Carta> AreaDeDescarte;
    public Jogo()
    {
        this.Jogadores = new Queue<Jogador>();
        this.Baralho = new Baralho();
        this.MonteDeCompras = new Stack<Carta>();
        this.AreaDeDescarte = new List<Carta>();
    }
    public Jogo(Queue<Jogador> jogadores, Baralho baralho, Stack<Carta> monte, List<Carta> area)
    {
        this.Jogadores = jogadores;
        this.Baralho = baralho;
        this.MonteDeCompras = monte;
        this.AreaDeDescarte = area;

    }
    public Jogo CriarJogo()
    {
        Console.Write("Insira a quantidade de joagdores: ");
        int quantidadeJogadores = int.Parse(Console.ReadLine());

        //Fila da ordem de jogadores
        Queue<Jogador> JogadoresDaPartida = new Queue<Jogador>(); // Criando fila

        for (int i = 0; i < quantidadeJogadores; i++)
        {
            Console.Write("Insira o nome do jogador: ");
            string nomeJogador = Console.ReadLine();

            Jogador jogador = new Jogador(nomeJogador);
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
        Baralho.baralho.Clear();

        //Inserir 4 cartas iniciais na area de descarte
        List<Carta> area = new List<Carta>();
        for (int i = 0; i < 4; i++)
        {
            area.Add(monte.Pop());
        }

        //Instanciar um novo jogo
        Jogo novoJogo = new Jogo(JogadoresDaPartida, baralho, monte, area);

       

        baralho.Imprimir();
        novoJogo.Imprimir();

        return novoJogo;

    }
    

    public void Imprimir()
    {
        foreach(Jogador Jogador in Jogadores)
        {
            Console.WriteLine(Jogador.Nome);
        }

        Console.WriteLine("Monte de compras");
        foreach(Carta carta in MonteDeCompras)
        {
            Console.Write(carta.Valor + carta.Naipe + " ");
        }

        Console.WriteLine("Area de descarte");
        foreach (Carta carta in AreaDeDescarte)
        {
            Console.Write(carta.Valor + carta.Naipe + " | ");
        }

    }



}
