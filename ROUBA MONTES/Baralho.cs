using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class Baralho
{
    public List<Carta> baralho;
    public Baralho()
    {
        this.baralho = new List<Carta>();
    }
    public void Embaralhar(List<Carta> baralho)
    {
        // Criar uma instância de Random para gerar números aleatórios.
        Random random = new Random();

        // Obter o número total de cartas no baralho.
        int n = baralho.Count;

        //Metodo principal para embaralhar
        while (n > 0)
        {   
            n--; // Decrementar o contador de cartas.
            int k = random.Next(n + 1); // Gerar um índice aleatório entre 0 e n.
            // Trocar a posição da carta no índice n com a carta no índice aleatório k.
            Carta carta = baralho[k];
            baralho[k] = baralho[n];
            baralho[n] = carta;
        }
    }

    public List<Carta> CriarBaralho(int quantidadeDeBaralhos)
    {
        //Codigo para reconhecer os naipes das cartas no terminal
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Inicializa o baralho com todas as cartas possíveis (52 cartas)
        char[] naipes = { '\u2665', '\u2666', '\u2663', '\u2660' };
        string[] valores = { /*"A", "2", "3", "4", "5", "6", "7", "8", "9",*/ "10", "J", "Q", "K" };

        //Até ser menor que a quantidade de baralhos escolhida pelo jogador
        for (int i = 0; i < quantidadeDeBaralhos; i++)
        {
            foreach (char naipe in naipes)
            {
                foreach (string valor in valores)
                {
                    baralho.Add(new Carta(valor, naipe));
                }
            }
        }
        Embaralhar(baralho);
        InserirLogs("Baralho criado e embaralhado");
        return baralho;
       

    }

    public void Imprimir()
    {
        foreach(Carta carta in baralho)
        {
            Console.WriteLine($"{carta.Valor} {carta.Naipe}");

        }
    }

    public void InserirLogs(string log)
    {
        try
        {

            using (StreamWriter arquivoLog = new StreamWriter("C:\\Users\\filip\\OneDrive\\Documentos\\PUC\\AED\\TRABALHO FINAL - AED\\ROUBA MONTES\\ROUBA MONTES\\Logs.txt", true, Encoding.UTF8))
            {
                arquivoLog.WriteLine(log);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }


}

