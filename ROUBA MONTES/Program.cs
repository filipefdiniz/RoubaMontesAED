using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        int opcao = 0;
        Jogo Jogo = new Jogo();
        InserirLogs("ROUBA MONTES - LOGS");

        do
        {
            
            Console.Clear();
            ImprimirTituloJogo();       

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("==== LET'S PLAY! ====");
            Console.ResetColor();
            Console.WriteLine("1 - INICIAR UM NOVO JOGO");
            Console.WriteLine("2 - VER HISTÓRICO");
            Console.WriteLine("3 - SAIR");
            Console.WriteLine("======================");
            Console.Write("Digite a opção desejada: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    //Instanciar novo jogo
                    InserirLogs("Iniciando um novo jogo.");
                    Jogo.CriarJogo();
                    //Iniciar o jogo depois de ter sido instanciado
                    Jogo.Jogar();
                    break;
                case 2:
                    InserirLogs("Ver histórico de um jogador");
                    Console.Write("Digite o ID do jogador que deseja ver o histórico de posições: ");
                    int idJogador = int.Parse(Console.ReadLine());
                    Jogo.ExibiRanking(idJogador);
                    break;
                default:
                    break;
            }
        } while (opcao != 3);


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



        void InserirLogs(string log)
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



}