using System;
using System.Collections.Generic;
class Jogador
{
    public string Nome;
    int Posicao;
    Carta CartaEmMao;
    Stack<Carta> CartasNoMonte;
    int QuantidadeCartasMonte;
    List<string> RankingPessoalPorPartida;

    public Jogador(string nome)
    {
        this.Nome = nome;
    }
}
