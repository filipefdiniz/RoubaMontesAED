using System;
using System.Collections;
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
        if (CartasNoMonte.Count > 0)
        {
            Carta carta = CartasNoMonte.Peek();
            Console.Write($"Carta do topo: ");
            Console.Write($"{carta.Valor}");
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

    public void ImprimirCartasDaMao()
    {
        Carta[] monteTemp = new Carta[CartasNoMonte.Count];

        for (int i = 0; i < monteTemp.Length; i++)
        {
            monteTemp[i] = CartasNoMonte.Pop();
        }
        ConverterCartasNoMonte(monteTemp);
        SelectionSort(monteTemp);
        ConverterCartasNoMonteLetras(monteTemp);
        for (int i = 0; i < monteTemp.Length; i++)
        {
            Console.Write(monteTemp[i].Valor);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(monteTemp[i].Naipe);
            Console.ResetColor();
            Console.Write(" | ");
        }

    }
    public Carta[] ConverterCartasNoMonte(Carta[] cartas)
    {
        foreach (Carta carta in cartas)
        {
            if (carta.Valor == "A")
            {
                carta.Valor = "1";
            }
            if (carta.Valor == "J")
            {
                carta.Valor = "11";
            }
            if (carta.Valor == "Q")
            {
                carta.Valor = "12";
            }
            if (carta.Valor == "K")
            {
                carta.Valor = "13";
            }
        }
        return cartas;
    }

    public Carta[] ConverterCartasNoMonteLetras(Carta[] cartas)
    {
        foreach (Carta carta in cartas)
        {
            if (carta.Valor == "1")
            {
                carta.Valor = "A";
            }
            if (carta.Valor == "11")
            {
                carta.Valor = "J";
            }
            if (carta.Valor == "12")
            {
                carta.Valor = "Q";
            }
            if (carta.Valor == "13")
            {
                carta.Valor = "K";
            }
        }
        return cartas;
    }
    static void SelectionSort(Carta[] vetor)
    {
        for (int i = 0; i < (vetor.Length - 1); i++)
        {
            int indiceMax = i;
            for (int j = (i + 1); j > vetor.Length; j++)
            {
                if (int.Parse(vetor[indiceMax].Valor) > int.Parse(vetor[j].Valor))
                {
                    indiceMax = j;
                }
            }
            if (indiceMax != i)
            {
                Carta temp = vetor[indiceMax];
                vetor[indiceMax] = vetor[i];
                vetor[i] = temp;
            }
        }
    }

}
