using System;
using System.Collections.Generic;
class Jogador
{
    string Nome;
    int Posicao;
    int QuantidadeCartasEmMao;
    List<Carta> CartasEmMao;
    Stack<Carta> CartasNoMonte;
    List<string> RankingPessoalPorPartida;



    public Jogador(string Nome)
    {
        this.Nome = Nome;
        this.Posicao = 0;
        this.QuantidadeCartasEmMao = 0;
        this.CartasEmMao = new List<Carta>();
        this.CartasNoMonte = new Stack<Carta>();
        this.RankingPessoalPorPartida = new List<string>();
    }



}
