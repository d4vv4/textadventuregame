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
        static void WelcomeMSG()
        {
            Console.WriteLine("\nWelcome to Text Adventure Game by David" +
                "\nTo move: use \"go to kitchen\" etc.\nTo look around: use keywork \"look\".\n\n");
        }
        public static void Main(string[] args)
        {
            try
            {
                WelcomeMSG();
                Player player = new Player();
                GameRound.NewMove(player);
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
                Console.WriteLine(string.Concat(method.DeclaringType.FullName, ".", method.Name));
            }
            Console.ReadLine();
        }
    }
}

