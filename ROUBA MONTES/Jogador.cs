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

    //==================================================================
    //Método para retornar informações atualizadas do jogador na rodada.
    public void RetornarInfo()
    {
        Console.Write($"ID: {Id} | ");
        Console.Write($"Jogador {Nome} | ");

        //Verifica se possui cartas no monte
        if (CartasNoMonte.Count > 0) {
            Carta carta = CartasNoMonte.Peek();
            Console.Write($"Carta do topo: ");
            Console.Write($"{ carta.Valor}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{carta.Naipe}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Monte vazio!");
            Console.ResetColor();

        }        
        Console.Write($" | Quantidade de cartas no monte: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{CartasNoMonte.Count}");
        Console.ResetColor();
    }

    //==============================================
    //Método para retornar o valor da carta do topo. 
    public string RetornarCartaDoTopo()
    {
        Carta cartaTopo = CartasNoMonte.Peek();
        return cartaTopo.Valor;
    }

 

    
}
