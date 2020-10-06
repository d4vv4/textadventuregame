using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class GameRound
    {
        public Player _player { get; set; }
        public Room _room { get; set; }


        List<string> validInputs = new List<string> { };
        List<string> possibleMovementInputs = new List<string> {"go to hallway", "go to kitchen", "go to livingroom", "go to storageroom",
                                                                "go to bedroom", "go to restroom", "go thru hole"};

        public Player NewInput(Player player, string input)
        {
            if (input == "look")
            {
                Console.WriteLine(player._Location._Description);
                NewInput(player, Console.ReadLine().ToLower());
            }
            else if (possibleMovementInputs.Contains(input))
            {
                var movement = possibleMovementInputs.Select(x => x.Contains(input));
                var tempCurrentLocation = player._Location._Name;
                //create new input containing only the name of room to be entered
                string tempInput = string.Join(" ", input.Split(' ').Skip(2));
                player._Location = Room.GetAllRooms().Where(x => x._Name.Contains(tempInput)).FirstOrDefault();
                NewMove(player);
            }
            else if (validInputs.Contains(input))
            {

            }
            return player;
        }
        public static void NewMove(Player player)
        {
            Thread.Sleep(1000);
            GameRound gameRound = new GameRound();
            Console.WriteLine("You are now in " + player._Location._Name);
            gameRound.NewInput(player, Console.ReadLine().ToLower());
        }
        public static void NewAction(Player player, string usedItem)
        {
            Thread.Sleep(1000);
            GameRound gameRound = new GameRound();
            Console.WriteLine("You use " + player._Location._Name);
            gameRound.NewInput(player, Console.ReadLine().ToLower());
        }
    }
}
