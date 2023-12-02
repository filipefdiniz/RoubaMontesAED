using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

class Jogo
{
    Queue<Jogador> Jogadores;
    Baralho Baralho;
    Stack<Carta> MonteDeCompras;
    List<Carta> AreaDeDescarte;
    Carta cartaDaVez;

    public Jogo()
    {
        this.Jogadores = new Queue<Jogador>();
        this.Baralho = new Baralho();
        this.MonteDeCompras = new Stack<Carta>();
        this.AreaDeDescarte = new List<Carta>();
        this.cartaDaVez = new Carta();
    }
    public Jogo(Queue<Jogador> jogadores, Baralho baralho, Stack<Carta> monte, List<Carta> area, Carta primeiraCarta)
    {
        this.Jogadores = jogadores;
        this.Baralho = baralho;
        this.MonteDeCompras = monte;
        this.AreaDeDescarte = area;
        this.cartaDaVez = primeiraCarta;
    }
    
    public void Jogar()
    {
        do
        {
            //Mostrar jogador
            Jogadas();
 

        }
        while (MonteDeCompras.Count == 0);

        //FinalizarJogo();
    }

    public void Jogadas()
    {
        
        Jogador jogadorDaVez = Jogadores.Dequeue();
        Console.WriteLine($"Jogador da vez: {jogadorDaVez.Nome}");


        //Comprar uma carta do monte

        MenuDeAcoes(jogadorDaVez);       
    }

    public void MenuDeAcoes(Jogador jogadorDaVez)
    {
        Console.WriteLine($"Carta da vez: {cartaDaVez.Valor} {cartaDaVez.Naipe}");

        //Mostrar área de descarte atual
        Console.WriteLine("Área de descarte: ");
        foreach (Carta carta in AreaDeDescarte)
        {
            Console.Write(carta.Valor + carta.Naipe + " | ");
        }
        Console.WriteLine();
        //Retornar informações dos jogadores na rodada atual
        Console.WriteLine("Monte dos jogadores: ");
        foreach (Jogador jogador in Jogadores)
        {
            jogador.RetornarInfo();
            Console.WriteLine();
        }
        

        Console.WriteLine();
        Console.WriteLine(
                "OPÇÕES DE AÇÃO\r\n" +
                "1 - Roubar monte de um jogador\r\n" +
                "2 - Roubar da mesa\r\n" +
                "3 - Descartar uma carta ");

        int opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Qual jogador você quer roubar a carta do monte? ");
                int idJogadorRoubado = int.Parse(Console.ReadLine());
                RoubarDoMonteDeJogador(idJogadorRoubado, jogadorDaVez);
                break;
            case 2:
                if (RoubarDaAreaDescarte(jogadorDaVez))
                {
                    Console.WriteLine("Roubo da área de descarte feito com sucesso. Jogue novamente!");
                    MenuDeAcoes(jogadorDaVez);
                }
                else
                {
                    Console.WriteLine("Carta não esta contida na área de descarte, tente novamente!");
                    MenuDeAcoes(jogadorDaVez);
                }
                break;
            case 3:
                DescartarCarta(jogadorDaVez);
                
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }

    public void RoubarDoMonteDeJogador(int idJogador, Jogador jogadorDaVez)
    {
        //Monte temporário
        Stack<Carta> MonteTemp = new Stack<Carta>();

        //Percorrer fila de jogadores
        foreach (Jogador jogadorRoubado in Jogadores)
        {
            //Verificação para achar o jogador escolhido que terá seu monte roubado
            if(jogadorRoubado.Id == idJogador)
            {
                Console.WriteLine("jogador = jogador por parametro");
                if (jogadorRoubado.CartasNoMonte.Count > 0)
                {
                    Console.WriteLine("cartas no monte > 0");
                    //Verificar se a carta da vez é igual a carta do topo do monte a ser roubado
                    if (cartaDaVez.Valor == jogadorRoubado.RetornarCartaDoTopo())
                    {
                        Console.WriteLine("existe a carta no monte do outro jogador");
                        //Empilhar as cartas do monte a ser roubado em um monte temporário
                        foreach (Carta carta in jogadorRoubado.CartasNoMonte.ToArray())
                        {
                            Console.WriteLine("entrou aqui sim");
                            MonteTemp.Push(jogadorRoubado.CartasNoMonte.Pop());
                        }

                        //Empilhar as cartas do monte temporário no monte da pessoa que o roubou
                        foreach (Carta carta in MonteTemp.ToArray()) 
                        {
                            jogadorDaVez.CartasNoMonte.Push(MonteTemp.Pop());
                        }

                        //Colocar a carta da vez no topo do monte da pessoa que roubou
                        jogadorDaVez.CartasNoMonte.Push(cartaDaVez);
                        MenuDeAcoes(jogadorDaVez);
                    }
                }
                else
                {
                    Console.WriteLine($"Jogador {jogadorRoubado.Nome} está sem cartas no monte. Escolha outro!");
                    MenuDeAcoes(jogadorDaVez);
                }
            }
        }
    }
    public bool RoubarDaAreaDescarte(Jogador jogadorDaVez)
    {
        string valorCarta = cartaDaVez.Valor;

        foreach (Carta carta in AreaDeDescarte)
        {
            if (carta.Valor == valorCarta)
            {
                Console.WriteLine("entrou");
                int posicaoCarta = AreaDeDescarte.IndexOf(carta);
                jogadorDaVez.CartasNoMonte.Push(AreaDeDescarte[posicaoCarta]);
                AreaDeDescarte.RemoveAt(posicaoCarta);
                jogadorDaVez.CartasNoMonte.Push(cartaDaVez);
                cartaDaVez = MonteDeCompras.Pop();
                return true;
            }
        }
        return false;     
    }
    public void DescartarCarta(Jogador jogadorDaVez)
    {
        AreaDeDescarte.Add(cartaDaVez);
        cartaDaVez = MonteDeCompras.Pop();
        //Voltar com o jogador da vez para o final da fila
        Jogadores.Enqueue(jogadorDaVez);
        Jogadas();
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
        

    }



}
