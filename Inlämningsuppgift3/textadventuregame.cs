using Inlämningsuppgift3.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inlämningsuppgift3
{
    public class textadventuregame
    {
        public static void WelcomeMSG()
        {
            Console.WriteLine("\nWelcome to Text Adventure Game by David" +
                "\nTo move: use \"go to kitchen\" etc." +
                "\nTo look around: use keyword \"look\"." +
                "\nTo open or search thru something: use keyword\"open\" infront of item." +
                "\nTo take up or drop items: use keywords \"take\" and \"drop\" followed by item name." +
                "\nTo use items: use keywords \"use\" and \"on\"." +
                "\nYou will begin in a hallway and your goal is to make your way out of the house." +
                "\nType \"ready\" when you are ready to go or \"exit\" if you want to exit\n");
        }
        public static void Main(string[] args)
        {
            try
            {
                WelcomeMSG();
                while (true)
                {
                    string input = Console.ReadLine().ToLower();
                    if (input == "ready")
                    {
                        Console.Clear();
                        WelcomeMSG();
                        Player player = new Player();
                        ConsoleSpinner spin = new ConsoleSpinner();
                        Console.Write("Setting up game...");
                        DateTime start = DateTime.Now;
                        while (DateTime.Now.Subtract(start).Seconds < 2)
                        {
                            Thread.Sleep(90);
                            spin.Turn();
                        }
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Game initialized, now entering house\n");
                        GameRound.NewGame(player);
                        break;
                    }
                    else if (input == "exit")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("type \"ready\" is you want to start, \"exit\" if you want to exit");
                    }
                }
            }
            catch (Exception exception)
            {
                var trace = new StackTrace(exception);
                var frame = trace.GetFrame(0);
                var method = frame.GetMethod();
                using (StreamWriter sw = new StreamWriter(@"errors.txt", true))
                {
                    sw.WriteLine(string.Concat(method.DeclaringType.FullName, ".", method.Name));
                }
            }
            Console.ReadLine();
        }
    }
}

