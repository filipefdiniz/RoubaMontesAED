using System;
using System.Collections.Generic;

class Jogo
{
    Queue<Jogador> Jogadores;
    Baralho Baralho;
    Stack<Carta> MonteDeCompras;
    List<Carta> AreaDeDescarte;
    Carta cartaDaVez;
    int contadorPartidas;

    //Construtor de um jogo deafult
    public Jogo()
    {
        this.Jogadores = new Queue<Jogador>();
        this.Baralho = new Baralho();
        this.MonteDeCompras = new Stack<Carta>();
        this.AreaDeDescarte = new List<Carta>();
        this.cartaDaVez = new Carta();
        this.contadorPartidas = 0;
    }

    //Construtor de um jogo com parâmetros
    public Jogo(Queue<Jogador> jogadores, Baralho baralho, Stack<Carta> monte, List<Carta> area, Carta primeiraCarta)
    {
        this.Jogadores = jogadores;
        this.Baralho = baralho;
        this.MonteDeCompras = monte;
        this.AreaDeDescarte = area;
        this.cartaDaVez = primeiraCarta;
        this.contadorPartidas = 0;
    }


    public void CriarJogo()
    {
        contadorPartidas++;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("=== INSIRA OS DADOS PARA COMEÇAR A PARTIDA ===");
        Console.ResetColor();

        //Verificar se é a primeira partida desde que foi iniciado no terminal
        if (contadorPartidas < 2)
        {
            Console.Write("Quantidade de jogadores da partida: ");
            int quantidadeJogadores = int.Parse(Console.ReadLine());

            //Fila da ordem de jogadores
            for (int i = 1; i <= quantidadeJogadores; i++)
            {
                Console.Write($"Insira o nome do {i}° Jogador: ");
                string nomeJogador = Console.ReadLine();

                Jogador jogador = new Jogador(i, nomeJogador);
                Jogadores.Enqueue(jogador);
            }
        }

        //Inserir quantidade de baralhos
        Console.Write("Quantidade de baralhos para a partida: ");
        int quantidadeBaralhos = int.Parse(Console.ReadLine());

        //Criar um baralho  
        List<Carta> lista = Baralho.CriarBaralho(quantidadeBaralhos);

        //Inserir as cartas do baralho no monte de compras     
        foreach (Carta carta in lista)
        {
            MonteDeCompras.Push(carta);
        }

        //Limpar a lista de cartas (as cartas estão inseridas na pilha)
        Baralho.baralho.Clear();

        //Inserir carta na carta da vez  
        cartaDaVez = MonteDeCompras.Pop();        
        
        Console.Clear();
        Imprimir();

    }


    //===================
    //Método para iniciar as rodadas.
    public void Jogar()
    {

        //Mostrar jogador
        do
        {
            Rodadas();
        } while (MonteDeCompras.Count > 0);


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
                    if (MonteDeCompras.Count > 0)
                    {
                        MenuDeAcoes(jogadorDaVez);
                    }
                    else
                    {
                        Jogadores.Enqueue(jogadorDaVez);
                        //FinalizarJogo();
                    }
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
        // Monte temporário para armazenar as cartas do monte do jogador a ser roubado
        Stack<Carta> MonteTemp = new Stack<Carta>();
        Queue<Jogador> jogadoresCopy = new Queue<Jogador>(Jogadores);

        // Percorrer fila de jogadores
        foreach (Jogador jogadorRoubado in jogadoresCopy)
        {
            // Verificação para achar o jogador escolhido que terá seu monte roubado
            if (jogadorRoubado.Id == idJogador)
            {
                // Verificar se a quantidade de cartas do monte do jogador a ser roubado é maior do que zero
                if (jogadorRoubado.CartasNoMonte.Count > 0)
                {
                    // Verificar se a carta da vez é igual a carta do topo do monte a ser roubado
                    if (cartaDaVez.Valor == jogadorRoubado.RetornarCartaDoTopo())
                    {
                        // Empilhar as cartas do monte a ser roubado em um monte temporário
                        foreach (Carta carta in jogadorRoubado.CartasNoMonte.ToArray())
                        {
                            MonteTemp.Push(jogadorRoubado.CartasNoMonte.Pop());
                        }

                        // Empilhar as cartas do monte temporário no monte da pessoa que o roubou
                        foreach (Carta carta in MonteTemp.ToArray())
                        {
                            jogadorDaVez.CartasNoMonte.Push(MonteTemp.Pop());
                        }

                        // Colocar a carta da vez no topo do monte da pessoa que roubou
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

                        // Atualizar a carta da vez após o roubo
                        if (MonteDeCompras.Count > 0)
                        {
                            cartaDaVez = MonteDeCompras.Pop();
                        }
                        else
                        {
                            Jogadores.Enqueue(jogadorDaVez);
                        }

                        // Voltar com o menu de ações para continuar a jogada
                        MenuDeAcoes(jogadorDaVez);
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        ImprimirTituloJogo();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Sua carta da vez não é igual à carta do monte do jogador escolhido. Tente novamente!");
                        Console.ResetColor();
                        Console.WriteLine();
                        MenuDeAcoes(jogadorDaVez);
                        return;
                    }
                }
                else // Verificação caso o jogador escolhido não tenha cartas no monte
                {
                    Console.Clear();
                    ImprimirTituloJogo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Jogador {jogadorRoubado.Nome} está sem cartas no monte. Tente novamente!");
                    Console.ResetColor();
                    Console.WriteLine();
                    MenuDeAcoes(jogadorDaVez);
                    return;
                }
            }
        }

        // Atualizar a carta da vez caso o jogador escolhido não seja encontrado
        if (MonteDeCompras.Count > 0)
        {
            cartaDaVez = MonteDeCompras.Pop();
        }
        

        Console.Clear();
        ImprimirTituloJogo();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Jogador com o ID {idJogador} não encontrado. Tente novamente!");
        Console.ResetColor();
        Console.WriteLine();
        MenuDeAcoes(jogadorDaVez);
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
                if (MonteDeCompras.Count > 0)
                {
                    cartaDaVez = MonteDeCompras.Pop();
                }

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
        if (MonteDeCompras.Count > 0)
        {
            cartaDaVez = MonteDeCompras.Pop();
            //Voltar com o jogador da vez para o final da fila
            Jogadores.Enqueue(jogadorDaVez);

            Console.Clear();
            ImprimirTituloJogo();
            //Reiniciar o menu para o próximo jogador realizar sua jogada
            Rodadas();
        }
        else
        {
            Jogadores.Enqueue(jogadorDaVez);
            //Adiciona na area de descarte
            //FinalizarJogo();
        }


    }

    //Metodo para quando o jogo acabar
    public void FinalizarJogo()
    {
        
        //Ordenar o ranking final
        Jogador[] RankingFinal = Jogadores.ToArray();
        SelectionSort(RankingFinal);

        Console.Clear();
        ImprimirTituloJogo();
        Console.WriteLine();
        Console.WriteLine("=====================");
        Console.WriteLine("==== FIM DE JOGO ====");
        Console.WriteLine("=====================\n");

        //Exibir ranking Final
        for (int i = 0; i < RankingFinal.Length; i++)
        {
            if (i == 0)
            {
                Console.WriteLine($"VENCEDOR DA PARTIDA: {RankingFinal[i].Nome} | Posicão: 1°Lugar | Quantidade de cartas no monte: {RankingFinal[i].CartasNoMonte.Count}");
                Console.Write("Cartas ordenadas: | ");
                RankingFinal[i].ImprimirCartasDaMao();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Jogador: {RankingFinal[i].Nome} | Posição: {i + 1}° Lugar | Quantidade de cartas em mão: {RankingFinal[i].CartasNoMonte.Count}");

            }    
        }

        //Armazenar posição na ultima partida do jogador
        
        for(int i = 0; i < RankingFinal.Length; i++)
        {

            RankingFinal[i].ArmazenarRankingFinal(i+1);
        }

        //foreach(Jogador jogador in Jogadores)
        //{
        //    jogador.CartasNoMonte.Clear();
        //}

        //AreaDeDescarte.Clear();
        //cartaDaVez = new Carta();

        Console.WriteLine("Pressione Qualquer Tecla Para Ir Ao Menu");
        Console.ReadKey();
    }

    public void ExibiRanking(int idJogador)
    {
        foreach(Jogador jogador in Jogadores)
        {
            int id = jogador.RetornarIdJogador();
            if(id == idJogador)
            {
                Console.WriteLine($"Jogador: {jogador.Nome}");
                jogador.ExibirHistóricoPessoal(id);
            }
        }
        Console.ReadKey();
    }
    public int QuantidadeJogadores()
    {
        return Jogadores.Count;
    }
    static void SelectionSort(Jogador[] vetor)
    {
        for (int i = 0; i < (vetor.Length - 1); i++)
        {
            int indiceMax = i;
            for (int j = (i + 1); j < vetor.Length; j++)
            {
                if (vetor[indiceMax].CartasNoMonte.Count < vetor[j].CartasNoMonte.Count)
                {
                    indiceMax = j;
                }
            }
            if (indiceMax != i)
            {
                Jogador temp = vetor[indiceMax];
                vetor[indiceMax] = vetor[i];
                vetor[i] = temp;
            }
        }
    }

    static void SelectionSort(Carta[] vetor)
    {
        for (int i = 0; i < (vetor.Length - 1); i++)
        {
            int indiceMax = i;
            for (int j = (i + 1); j < vetor.Length; j++)
            {
                if (int.Parse(vetor[indiceMax].Valor) < int.Parse(vetor[j].Valor))
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
    void Quicksort(Carta[] array, int esq, int dir)
    {
        int i = esq, j = dir;
        int pivo = int.Parse(array[(esq + dir) / 2].Valor);

        while (i <= j)
        {
            while (i < array.Length && int.Parse(array[i].Valor) < pivo)
                i++;

            while (j >= 0 && int.Parse(array[j].Valor) > pivo)
                j--;
            if (i <= j)
            {
                Trocar(array, i, j);
                i++;
                j--;
            }
        }
        if (esq < j)
            Quicksort(array, esq, j);
        if (i < dir)
            Quicksort(array, i, dir);
    }
    void Trocar(Carta[] array, int i, int j)
    {
        Carta temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    void Quicksort(Jogador[] array, int esq, int dir)
    {
        int i = esq, j = dir;
        int pivo = array[(esq + dir) / 2].CartasNoMonte.Count;
        while (i <= j)
        {
            while (array[i].CartasNoMonte.Count < pivo)
                i++;
            while (array[j].CartasNoMonte.Count > pivo)
                j--;
            if (i <= j)
            {
                Trocar(array, i, j);
                i++;
                j--;
            }
        }
        if (esq < j)
            Quicksort(array, esq, j);
        if (i < dir)
            Quicksort(array, i, dir);
    }
    void Trocar(Jogador[] array, int i, int j)
    {
        Jogador temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    //Imprimir titulo rouba montes estilizado
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

    //imprimir informações iniciais
    public void Imprimir()
    {
        ImprimirTituloJogo();

        Console.WriteLine("=== Jogadores da rodada ===");
        foreach (Jogador Jogador in Jogadores)
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
