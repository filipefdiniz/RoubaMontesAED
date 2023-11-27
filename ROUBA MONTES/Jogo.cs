using System;
using System.Collections.Generic;

class Jogo
{
    List<Jogador> Jogadores;
    List<Carta> Baralho;
    Stack<Carta> MonteDeCompras;
    List<Carta> AreaDeDescarte;

    public Jogo(List<Jogador> jogadores, List<Carta> baralho, Stack<Carta> monteDeCompras, List<Carta> areaDeDescarte)
    {
        this.Jogadores = jogadores;
        this.Baralho = baralho;
        this.MonteDeCompras = monteDeCompras;
        this.AreaDeDescarte = areaDeDescarte;

    }


}
