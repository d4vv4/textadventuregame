using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class GameRound
    {
        public Player _player { get; set; }
        public Room _room { get; set; }

        bool drawerOpen = false;
        bool cabinetOpen = false;
        bool shelfOpen = false;
        bool rubbishSearched = false;

        List<string> validInputs = new List<string> { };

        List<string> possibleMovementInputs = new List<string> {"go to hallway", "go to kitchen", "go to living room", "go to storage room",
                                                                "go to bedroom", "go to restroom", "go thru hole"};

        List<string> possibleUseInputs = new List<string> { "open drawer","take key","search thru pile of rubbish","take stick","take empty can of bean",
            "use stick on shelf","take shield","take bag of fertiliser","open cabinet","take timer","use bag of fertiliser on empty can of bean",
            "use timer on bomb","use bomb on crack" };

        public Player NewInput(Player player, string input)
        {
            if (input == "look")
            {
                Console.WriteLine(player._Location._Description);
                NewInput(player, Console.ReadLine().ToLower());
            }
            //if input contains movement input
            else if (possibleMovementInputs.Contains(input))
            {
                //create new input containing only the name of room to be entered
                string tempInput = string.Join(" ", input.Split(' ').Skip(2));
                Room currentRoom = player._Location;
                List<Room> connectedRooms = player._Location._Connected;

                //if movement input is valid because of connected rooms
                if (player._Location._Connected.Any(x => x._Name == tempInput))
                {
                    player._Location = connectedRooms.Where(x => x._Name == tempInput).FirstOrDefault();
                    NewMove(player);
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Invalid input, room " + player._Location._Name + " and " + tempInput + " is not connected");
                    NewMoveWithOutPrint(player);
                }
            }
            else if (input.Contains(player._Location._UsableFurniture._Name))
            {
                if (input == "open " + player._Location._UsableFurniture._Name)
                {
                    if (player._Location._UsableFurniture._Name == "drawer")
                    {
                        drawerOpen = true;
                        Thread.Sleep(1000);
                        string items = Room.GetCurrentItems(player);

                        Console.Write("you open " + player._Location._UsableFurniture._Name + ", inside it there is a " + items);
                    }
                    else if (player._Location._UsableFurniture._Name == "pile of rubbish")
                    {
                        rubbishSearched = true;
                        Thread.Sleep(1000);
                        string items = Room.GetCurrentItems(player);

                        Console.Write("you open " + player._Location._UsableFurniture._Name + ", inside it there is a " + items);
                    }
                    else if (player._Location._UsableFurniture._Name == "shelf")
                    {
                        shelfOpen = true;
                        Thread.Sleep(1000);
                        string items = Room.GetCurrentItems(player);

                        Console.Write("you open " + player._Location._UsableFurniture._Name + ", inside it there is a " + items);
                    }
                    else if (player._Location._UsableFurniture._Name == "cabinet")
                    {
                        cabinetOpen = true;
                        Thread.Sleep(1000);
                        string items = Room.GetCurrentItems(player);

                        Console.Write("you open " + player._Location._UsableFurniture._Name + ", inside it there is a " + items);
                    }
                }
            }
            else
            {
                Console.WriteLine("invalid input, try again");
                NewMoveWithOutPrint(player);
            }
            return player;
        }
        public static void NewMove(Player player)
        {
            Thread.Sleep(1000);
            GameRound gameRound = new GameRound();
            Console.WriteLine("You are now in the " + player._Location._Name);
            gameRound.NewInput(player, Console.ReadLine().ToLower());
        }
        public static void NewMoveWithOutPrint(Player player)
        {
            Thread.Sleep(1000);
            GameRound gameRound = new GameRound();
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
