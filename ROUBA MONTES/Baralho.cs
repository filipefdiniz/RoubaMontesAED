using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class Baralho
{
    private List<Carta> baralho;
    public Baralho()
    {
        this.baralho = new List<Carta>();
    }

    public List<Carta> CriarBaralho(int quantidadeDeBaralhos)
    {
        List<Carta> cartas = new List<Carta>();

        // Inicializa o baralho com todas as cartas possíveis (52 cartas)
        string[] naipes = { "♥", "♦️", "♣️", "♠️" };
        string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        for (int i = 0; i < quantidadeDeBaralhos; i++)
        {
            foreach (string naipe in naipes)
            {
                foreach (string valor in valores)
                {
                    cartas.Add(new Carta(valor, naipe));
                }
            }
        }

        return cartas;
    }

    public void Embaralhar(List<Carta> baralho)
    {
        Random random = new Random();
        int n = baralho.Count;

        // Embaralha as cartas usando o algoritmo de Fisher-Yates
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Carta carta = baralho[k];
            baralho[k] = baralho[n];
            baralho[n] = carta;
        }
    }
}

