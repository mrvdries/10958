using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace _10958
{
    class Generator
    {
        private int[] vals;

        private char[] ops = new char[] { '+', '-', '*', '/', '^', '|' };

        public Generator()
        {
            //default values are 1 to 9
            vals = new int[9];
            int i = 0;
            while (i < 9)
            {
                vals[i] = ++i;
            }
        }

        public Generator(int[] values)
        {
            vals = values;
        }

        public void Reset()
        {
            using (StreamWriter writetext = new StreamWriter("iteration.txt"))
            {
                writetext.WriteLine();
                writetext.Close();
            }
        }

        public int GetIteration()
        {
            int iteration = 0;
            string read = "0";
            try
            {
                Console.WriteLine(">: Trying to read iteration.txt to find last iteration..");
                using (StreamReader sr = new StreamReader("iteration.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        read = sr.ReadLine();
                    }
                    iteration = Convert.ToInt32(read);
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!: The file could not be read:");
                Console.WriteLine("   " + e.Message);
                Console.WriteLine("?: Do you wish to create a new file instead? (y/n)");
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey();
                    Console.WriteLine();
                } while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N);
                if (cki.Key == ConsoleKey.Y)
                {
                    using (StreamWriter writetext = new StreamWriter("iteration.txt"))
                    {
                        writetext.WriteLine(iteration);
                        writetext.Close();
                    }
                    Console.WriteLine("   The file iteration.txt was successfully created");
                    Console.WriteLine();
                }
            }
            return iteration;
        }

        public void Generate()
        {
            int iteration = 0;
            string read = "0";
            try
            {
                Console.WriteLine(">: Trying to read iteration.txt to find last iteration..");
                using (StreamReader sr = new StreamReader("iteration.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        read = sr.ReadLine();
                    }
                    iteration = Convert.ToInt32(read);
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!: The file could not be read:");
                Console.WriteLine("   " + e.Message);
                Console.WriteLine("?: Do you wish to create a new file instead? (y/n)");
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey();
                    Console.WriteLine();
                } while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N);
                if(cki.Key == ConsoleKey.Y)
                {
                    using (StreamWriter writetext = new StreamWriter("iteration.txt"))
                    {
                        writetext.WriteLine(iteration);
                        writetext.Close();
                    }
                    Console.WriteLine("   The file iteration.txt was successfully created");
                    Console.WriteLine();
                }
            }
            Console.WriteLine(">: Starting generation from iteration: " + iteration);
            Generate(iteration);
        }

        public void Generate(int iter)
        {
            int iteration = iter;
            //Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            //Console.KeyAvailable
            ConsoleKeyInfo cki;
            string read = "";
            try
            {
                Console.WriteLine(">: Trying to read operations.txt..");
                using (StreamReader sr = new StreamReader("operations.txt"))
                {
                    int i = 0;
                    while (i != iteration)
                    {
                        read = sr.ReadLine();
                        i++;
                    }
                    Brackets brackets = new Brackets();
                    ArrayList list = brackets.Generate();
                    do
                    {
                        Console.WriteLine("\ni: Press any key to pause");
                        string command = "";
                        Command com;
                        while (!Console.KeyAvailable)
                        {
                            Console.WriteLine(iteration);
                            read = sr.ReadLine();
                            command = GenerateCommand(read);
                            Console.WriteLine(command);
                            foreach (string[] s in list)
                            {
                                string commandBracketed = s[0];
                                for(int j = 0; j < 15; j++)
                                {
                                    commandBracketed += command[j];
                                    if ((j % 2) == 0)
                                    {
                                        commandBracketed += s[j/2+1];
                                    }
                                }
                                //Console.WriteLine(commandBracketed);
                                com = new Command(commandBracketed);
                                double result = -1;

                                try
                                {
                                    result = com.Solve();
                                }
                                catch(System.OverflowException e)
                                {
                                    Console.WriteLine("Value out of range");
                                }
                                catch(System.FormatException e)
                                {
                                    Console.WriteLine("Can't concatenate infinity");
                                }
                                Console.WriteLine(commandBracketed + "=" + result);
                                if (result > 10957 && result < 10959)
                                {
                                    Console.WriteLine("=) This result is within acceptable range.");
                                    Console.WriteLine("   Writing to results.txt..");
                                    using (StreamWriter writetext = new StreamWriter("results.txt", true))
                                    {
                                        writetext.WriteLine("iteration:" + iteration + ", command:" + command + ", result:" + result);
                                        writetext.Close();
                                    }
                                    Thread.Sleep(5000);
                                }
                            }
                            
                            iteration++;
                        }
                        cki = Console.ReadKey(true);
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                        Console.WriteLine(">: The application was paused by user input.");
                        Console.WriteLine("   Current iteration:" + iteration);
                        Console.WriteLine();
                        try
                        {
                            using (StreamReader results = new StreamReader("results.txt"))
                            {
                                Console.WriteLine(">: The following combinations have been found that are close to 10958:");
                                while (results.EndOfStream == false)
                                {
                                    Console.WriteLine(results.ReadLine());
                                }
                                results.Close();
                                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("!: No results were found yet that are close to 10958.");
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        }
                        Console.WriteLine("\n>: Press any key to continue; press the 'x' key to return to menu.");
                        
                        cki = Console.ReadKey();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    } while (cki.Key != ConsoleKey.X && sr.EndOfStream == false);
                    if (sr.EndOfStream) { Console.WriteLine("!: The end of the list of operations has been reached."); }
                    sr.Close();
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.GetType());
                Console.WriteLine("!: The file could not be read:");
                Console.WriteLine("   "+e.Message);
                if(e.Message == "Object reference not set to an instance of an object.")
                {
                    Console.WriteLine();
                    Console.WriteLine("!: The end of the list of operations has been reached.");
                    Console.WriteLine("   Application will return to menu after key press..");
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    cki = Console.ReadKey();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                }
                else
                {
                    Console.WriteLine("?: Do you wish to create a new file instead? (y/n)");
                    Console.WriteLine();
                    do
                    {
                        cki = Console.ReadKey();
                        Console.WriteLine();
                    } while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N);
                    if (cki.Key == ConsoleKey.Y)
                    {
                        GenerateOpList();
                        Console.WriteLine("   The file operations.txt was successfully created");
                    }
                }
            }
            using (StreamWriter writetext = new StreamWriter("iteration.txt", true))
            {
                writetext.WriteLine(iteration);
                writetext.Close();
            }
        }

        public string GenerateCommand(string read)
        {
            string command = ((char)('0' + vals[0])).ToString();
            for(int i = 0; i < vals.Length-1; i++)
            {
                command+=read[i];
                command+=(char)('0' + vals[i + 1]);
            }
            return command;
        }

        public void GenerateOpList()
        {
            using (StreamWriter writetext = new StreamWriter("operations.txt"))
            {
                string output = "";
                for (int a = 0; a < ops.Length; a++)
                {
                    for (int b = 0; b < ops.Length; b++)
                    {
                        for (int c = 0; c < ops.Length; c++)
                        {
                            for (int d = 0; d < ops.Length; d++)
                            {
                                for (int e = 0; e < ops.Length; e++)
                                {
                                    for (int f = 0; f < ops.Length; f++)
                                    {
                                        for (int g = 0; g < ops.Length; g++)
                                        {
                                            for (int h = 0; h < ops.Length; h++)
                                            {
                                                output = "" + ops[a] + ops[b] + ops[c] + ops[d] + ops[e] + ops[f] + ops[g] + ops[h];
                                                writetext.WriteLine(output);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
