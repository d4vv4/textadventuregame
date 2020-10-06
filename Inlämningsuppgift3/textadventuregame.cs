using Inlämningsuppgift3.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3
{
    class textadventuregame
    {
        static void Main(string[] args)
        {
            try
            {
                bool game = true;
                List<Room> rooms = Room.GetAllRooms();
                Player player = new Player(rooms[0]);
                while (game)
                {
                    Console.WriteLine("You are currently in " + player._Location._Name + ", " + player._Location._Description +
                        "\nWhere do you want to go? Use arrow keys");
                    player.NewInput(player,Console.ReadLine().ToLower());
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
                Console.WriteLine(string.Concat(method.DeclaringType.FullName, ".", method.Name));
            }
            
            Console.ReadLine();
        }
    }
}

