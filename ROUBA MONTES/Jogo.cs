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

    //Construtor de um jogo deafult
    public Jogo()
    {
        this.Jogadores = new Queue<Jogador>();
        this.Baralho = new Baralho();
        this.MonteDeCompras = new Stack<Carta>();
        this.AreaDeDescarte = new List<Carta>();
        this.cartaDaVez = new Carta();
    }

    //Construtor de um jogo com parâmetros
    public Jogo(Queue<Jogador> jogadores, Baralho baralho, Stack<Carta> monte, List<Carta> area, Carta primeiraCarta)
    {
        this.Jogadores = jogadores;
        this.Baralho = baralho;
        this.MonteDeCompras = monte;
        this.AreaDeDescarte = area;
        this.cartaDaVez = primeiraCarta;
    }
    
    //===================
    //Método para iniciar as rodadas.
    public void Jogar()
    {
        do
        {
            //Mostrar jogador
            Rodadas();
        }
        while (MonteDeCompras.Count == 0);

        FinalizarJogo();
    }

    //=================================================================
    //Método para reiniciar as rodadas e mandar para o próximo jogador.
    public void Rodadas()
    { 
        Jogador jogadorDaVez = Jogadores.Dequeue();    
        MenuDeAcoes(jogadorDaVez);       
    }

    //===================================
    //Método de menu de ações de jogadas.
    public void MenuDeAcoes(Jogador jogadorDaVez)
    {
        Console.Write($"Jogador da vez:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" {jogadorDaVez.Nome}");
        Console.ResetColor();
        Console.Write($"Carta da vez: ");
        Console.Write($"{cartaDaVez.Valor}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{cartaDaVez.Naipe}");
        Console.ResetColor();
        Console.WriteLine();

        //Mostrar área de descarte atual
        Console.WriteLine("=== ÁREA DE DESCARTE ===");
        Console.Write("| ");
        foreach (Carta carta in AreaDeDescarte)
        {
            Console.Write(carta.Valor + carta.Naipe + " | ");
        }
        Console.WriteLine();
        Console.WriteLine();
        //Retornar informações dos jogadores na rodada atual
        Console.WriteLine("==== MONTE DOS JOGADORES ==== ");
        foreach (Jogador jogador in Jogadores)
        {
            jogador.RetornarInfo();
            Console.WriteLine();
        }
        Console.WriteLine();

        //Menu para exibir opções de jogadas
        Console.WriteLine(
                "ESCOLHA SUA JOGADA:\r\n" +
                "1 - Roubar monte de um jogador\r\n" +
                "2 - Roubar da mesa\r\n" +
                "3 - Descartar uma carta ");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("OPÇÃO DE JOGADA: ");
        Console.ResetColor();

        int opcao = int.Parse(Console.ReadLine());

        //Switch case para chamar os métodos das jogadas durante a rodada
        switch (opcao)
        {
            case 1:
                Console.WriteLine("----------------------------------------------");
                Console.Write("Digite o ID do jogador que quer roubar o monte: ");
                int idJogadorRoubado = int.Parse(Console.ReadLine());
                RoubarDoMonteDeJogador(idJogadorRoubado, jogadorDaVez);
                break;
            case 2:
                if (RoubarDaAreaDescarte(jogadorDaVez))
                {
                    Console.Clear();
                    ImprimirTituloJogo();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Roubo da área de descarte feito com sucesso. Jogue novamente!");
                    Console.ResetColor();
                    Console.WriteLine();   
                    MenuDeAcoes(jogadorDaVez);
                }
                else
                {
                    Console.Clear();
                    ImprimirTituloJogo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Carta não esta contida na área de descarte, tente novamente!");
                    Console.ResetColor();
                    Console.WriteLine();
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

    //====================================================
    //Método para roubar o monte de um jogador específico.
    public void RoubarDoMonteDeJogador(int idJogador, Jogador jogadorDaVez)
    {
        //Monte temporário para armazenar as cartas do monte do jogador a ser roubado
        Stack<Carta> MonteTemp = new Stack<Carta>();

        //Percorrer fila de jogadores
        foreach (Jogador jogadorRoubado in Jogadores)
        {
            //Verificação para achar o jogador escolhido que terá seu monte roubado
            if(jogadorRoubado.Id == idJogador)
            {
                //Verificar se a quantidade de cartas do monte do jogador a ser roubado é maior do que zero
                if (jogadorRoubado.CartasNoMonte.Count > 0)
                {
                    //Verificar se a carta da vez é igual a carta do topo do monte a ser roubado
                    if (cartaDaVez.Valor == jogadorRoubado.RetornarCartaDoTopo())
                    {
                        //Empilhar as cartas do monte a ser roubado em um monte temporário
                        foreach (Carta carta in jogadorRoubado.CartasNoMonte.ToArray())
                        {   
                            MonteTemp.Push(jogadorRoubado.CartasNoMonte.Pop());
                        }

                        //Empilhar as cartas do monte temporário no monte da pessoa que o roubou
                        foreach (Carta carta in MonteTemp.ToArray()) 
                        {
                            jogadorDaVez.CartasNoMonte.Push(MonteTemp.Pop());
                        }

                        //Colocar a carta da vez no topo do monte da pessoa que roubou
                        jogadorDaVez.CartasNoMonte.Push(cartaDaVez);

                        Console.Clear();
                        ImprimirTituloJogo();
                        Console.Write($"Monte do jogador ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"{jogadorRoubado.Nome} ");
                        Console.ResetColor();
                        Console.Write($"roubado pelo jogador ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"{jogadorDaVez.Nome}!");
                        Console.ResetColor();
                        Console.WriteLine();
                        //Voltar com o menu de ações para continuar a jogada
                        MenuDeAcoes(jogadorDaVez);
                    }
                    else
                    {
                        Console.Clear();
                        ImprimirTituloJogo();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Sua carta da vez não é igual a carta do monte do jogador escolhido.Tente novamente!");
                        Console.ResetColor();
                        Console.WriteLine();
                        MenuDeAcoes(jogadorDaVez);
                    }
                }
                else //Verificação caso o jogador escolhido nao tenha cartas no monte
                {
                    Console.Clear();
                    ImprimirTituloJogo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Jogador {jogadorRoubado.Nome} está sem cartas no monte. Tente novamente!");
                    Console.ResetColor();
                    Console.WriteLine();
                    MenuDeAcoes(jogadorDaVez);
                }
            }
        }
    }
    
    //=================================================
    //Método para roubar uma carta da Área de descarte.
    public bool RoubarDaAreaDescarte(Jogador jogadorDaVez)
    {
        //Valor da carta da vez a ser comparada
        string valorCarta = cartaDaVez.Valor;

        //Percorrer a lista de cartas da área de descarte
        foreach (Carta carta in AreaDeDescarte)
        {
            //Verificar quando e se a carta da vez for igual a alguma carta da área de descarte
            if (carta.Valor == valorCarta)
            {
                //Instancia a posição da carta na lista
                int posicaoCarta = AreaDeDescarte.IndexOf(carta);

                //Insere a carta especificada pela posição, no monte do jogador que a está roubando
                jogadorDaVez.CartasNoMonte.Push(AreaDeDescarte[posicaoCarta]);

                //Remover a carta roubada da lista de cartas da área de descarte
                AreaDeDescarte.RemoveAt(posicaoCarta);

                //Inserir a carta da vez - carta comprada - no monte de quem realizou o roubo
                jogadorDaVez.CartasNoMonte.Push(cartaDaVez);

                //Atualiza a carta da vez
                cartaDaVez = MonteDeCompras.Pop();

                return true;
            }
        }
        return false;     
    }

    //======================================================================
    //Método para descartar a carta da vez. Quando não a opções para roubar.
    public void DescartarCarta(Jogador jogadorDaVez)
    {
        //Adicionar a carta a ser descartada a lista de cartas da área de descarte
        AreaDeDescarte.Add(cartaDaVez);

        //Atualizar a carta da vez - Comprada do monte de compras
        cartaDaVez = MonteDeCompras.Pop();

        //Voltar com o jogador da vez para o final da fila
        Jogadores.Enqueue(jogadorDaVez);

        Console.Clear();
        ImprimirTituloJogo();
        //Reiniciar o menu para o próximo jogador realizar sua jogada
        Rodadas();
    }

    public void FinalizarJogo()
    {

    }

    static void ImprimirTituloJogo()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(@"
██████╗░░█████╗░██╗░░░██╗██████╗░░█████╗░  ███╗░░░███╗░█████╗░███╗░░██╗████████╗███████╗░██████╗
██╔══██╗██╔══██╗██║░░░██║██╔══██╗██╔══██╗  ████╗░████║██╔══██╗████╗░██║╚══██╔══╝██╔════╝██╔════╝
██████╔╝██║░░██║██║░░░██║██████╦╝███████║  ██╔████╔██║██║░░██║██╔██╗██║░░░██║░░░█████╗░░╚█████╗░
██╔══██╗██║░░██║██║░░░██║██╔══██╗██╔══██║  ██║╚██╔╝██║██║░░██║██║╚████║░░░██║░░░██╔══╝░░░╚═══██╗
██║░░██║╚█████╔╝╚██████╔╝██████╦╝██║░░██║  ██║░╚═╝░██║╚█████╔╝██║░╚███║░░░██║░░░███████╗██████╔╝
╚═╝░░╚═╝░╚════╝░░╚═════╝░╚═════╝░╚═╝░░╚═╝  ╚═╝░░░░░╚═╝░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░╚══════╝╚═════╝░");
        Console.ResetColor();
        Console.WriteLine("=================================================================================================");
        Console.WriteLine("======================================= | \u2665 | \u2666 | \u2663 | \u2660 | =======================================");


        Console.WriteLine();
    }

    public void Imprimir()
    {
        ImprimirTituloJogo();
        
        Console.WriteLine("=== Jogadores da rodada ===");
        foreach(Jogador Jogador in Jogadores)
        {
            int i = 1;
            Console.WriteLine($"Jogador {i}: {Jogador.Nome} ");
            i++;
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=-=-=-=-= JOGO INICIADO =-=-=-=-=-=");
        Console.ResetColor();
        Console.WriteLine();

    }



}
