using System;
using System.Collections.Generic;
class Jogador
{
    public int Id;
    public string Nome;
    int Posicao;
    public Stack<Carta> CartasNoMonte;
    List<string> RankingPessoalPorPartida;

    
    public Jogador(int id, string nome)
    {
        this.Id = id;
        this.Nome = nome;
        this.Posicao = 0;
        this.CartasNoMonte = new Stack<Carta>();
        this.RankingPessoalPorPartida = new List<string>();
    }

    public void RetornarInfo()
    {
        Console.Write($"ID: {Id} | ");
        Console.Write($"Jogador {Nome} | ");
        if (CartasNoMonte.Count > 0) {
            Carta carta = RetornarCartaDoTopo();
            Console.WriteLine($"Carta do topo: {carta.Valor} {carta.Naipe} | ");
         }
        else
        {
            Console.Write("Monte vazio! | ");
        }
        Console.Write($"Quantidade de cartas no monte: {CartasNoMonte.Count}");

    }

    public Carta RetornarCartaDoTopo()
    {
        Carta cartaTopo = CartasNoMonte.Peek();
        return cartaTopo;
    }

 

    
}
