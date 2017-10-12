using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _10958
{
    class Program
    {
        private static string author = "Maxime R. Van Driessche";

        static void Main(string[] args)
        {
            bool menu = true;
            ConsoleKeyInfo cki;
            while (menu)
            {
                Console.WriteLine("=============================================================");
                Console.WriteLine("~ This application is an attempt to solve the 10958 problem ~");
                Console.WriteLine("~ from the Crazy Sequential Representation publication by   ~");
                Console.WriteLine("~ Inder J.Taneja                                            ~");
                Console.WriteLine(" Author: " + author);
                Console.WriteLine("=============================================================");
                Console.WriteLine();
                Console.WriteLine("---------------------------MENU------------------------------");
                Console.WriteLine("                        S: Start");
                Console.WriteLine("                        R: Reset");
                Console.WriteLine("                        E: Exit");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.WriteLine(">: Enter menu option: (S,R,E)");
                cki = Console.ReadKey();
                Console.WriteLine();
                Generator gen = new Generator();
                if (cki.Key == ConsoleKey.S)
                {
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine(">: Generation will continue from iteration : " + gen.GetIteration());
                    Console.WriteLine("   !!While the application is running press any key to pause!!");
                    Console.WriteLine();
                    Console.WriteLine(">: Press any key to start");
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.ReadKey();
                    gen.Generate();
                }
                if (cki.Key == ConsoleKey.R)
                {
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("?: Are you sure you want to delete iteration.txt?(y/n)");
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    do
                    {
                        cki = Console.ReadKey();
                        Console.WriteLine();
                    } while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N);
                    if (cki.Key == ConsoleKey.Y)
                    {
                        gen.Reset();
                        Console.WriteLine("   The file iteration.txt was successfully deleted");
                        Console.WriteLine("   Returning to menu..");
                        Thread.Sleep(2000);
                    }
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                }
                if (cki.Key == ConsoleKey.E)
                {
                    Console.WriteLine(">: Application will be terminated.");
                    menu = false;
                }
            }
        }
    }
}
