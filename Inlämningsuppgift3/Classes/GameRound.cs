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
        public Player _Player { get; set; }
        public Room _Room { get; set; }
        public bool _DoorIsLocked { get; set; }

        public GameRound(Player player,Room room,bool doorIsLocked)
        {
            _Player = player;
            _Room = room;
            _DoorIsLocked = doorIsLocked;
        }

        List<string> possibleMovementInputs = new List<string> {"go to hallway", "go to kitchen", "go to living room", "go to storage room",
                                                                "go to bedroom", "go to restroom", "go thru hole"};

        List<string> possibleUseInputs = new List<string> { "use key on door", "use crowbar on wardrobe", "use shield", "use bag of fertiliser on can",
                                                            "use timer on bomb","use bomb on timer", "use can on bag of fertiliser" };

        public Player NewInput(Player player, string input)
        {
            //if input is look
            if (input == "look")
            {
                Console.WriteLine(player._Location._Description);
                NewInput(player, Console.ReadLine().ToLower());
            }
            //if input is items
            if (input == "items")
            {
                Console.WriteLine("your current items are:");
                foreach (var item in player._Items)
                {
                    Console.WriteLine(item);
                }
                NewInput(player, Console.ReadLine().ToLower());
            }
            //if input contains movement input
            else if (possibleMovementInputs.Contains(input))
            {
                //create new input containing only the name of room to be entered
                string tempInput = string.Join(" ", input.Split(' ').Skip(2));
                List<Room> connectedRooms = player._Location._Connected;

                //if movement input is valid because of connected rooms
                if (player._Location._Connected.Any(x => x._Name == tempInput))
                {
                    player._Location._UsableFurniture._IsOpen = false;
                    player._Location = connectedRooms.Where(x => x._Name == tempInput).FirstOrDefault();
                    Console.WriteLine("You are now in the " + player._Location._Name);
                    NewInput(player, Console.ReadLine().ToLower());
                }
                else if (player._Location._Name == tempInput)
                {
                    Console.WriteLine("Invalid input, you are already in " + player._Location._Name);
                    NewInput(player, Console.ReadLine().ToLower());
                }
                else
                {
                    Console.WriteLine("Invalid input, room " + player._Location._Name + " and " + tempInput + " is not connected");
                    NewInput(player, Console.ReadLine().ToLower());
                }
            }
            //if input is about opening usableFurniture
            else if (input == "open " + player._Location._UsableFurniture._Name)
            {
                //special for door since it is locked before you unlock it
                if (player._Location._UsableFurniture._Name == "door")
                {
                    if (player._Items.Any(x => x._Name == "crowbar"))
                    {
                        player._Location._UsableFurniture._IsOpen = true;
                        PrintItems(player);
                        NewInput(player, Console.ReadLine().ToLower());
                    }
                    else
                    {
                        Console.WriteLine("you need something to break the wardrobe open");
                        NewInput(player, Console.ReadLine().ToLower());
                    }
                }
                //special for wardrobe since you need crowbar
                if (player._Location._UsableFurniture._Name == "wardrobe")
                {
                    if (player._Items.Any(x => x._Name == "crowbar"))
                    {
                        player._Location._UsableFurniture._IsOpen = true;
                        PrintItems(player);
                        NewInput(player, Console.ReadLine().ToLower());
                    }
                    else
                    {
                        Console.WriteLine("you need something to break the wardrobe open");
                        NewInput(player, Console.ReadLine().ToLower());
                    }
                }
                else
                {
                    player._Location._UsableFurniture._IsOpen = true;
                    PrintItems(player);
                    NewInput(player, Console.ReadLine().ToLower());
                }

            }
            //take/drop input
            else if (input.Contains("take") || input.Contains("drop"))
            {
                try
                {
                    if (player._Location._UsableFurniture._IsOpen == true)
                    {
                        TakeOrDrop(player, input);
                        NewInput(player, Console.ReadLine().ToLower());
                    }
                    else
                    {
                        InvalidInput(player);
                        NewInput(player, Console.ReadLine().ToLower());
                    }

                }
                catch (Exception)
                {
                    InvalidInput(player);
                    NewInput(player, Console.ReadLine().ToLower());
                }
            }
            //possible combination and uses of items
            else if (possibleUseInputs.Contains(input))
            {
                //single way uses
                if (input == "use key on door")
                {
                    if (true)
                    {

                    }
                    else
                    {

                    }
                }
                else if (input == "use crowbar on wardrobe")
                {

                }
                else if (input == "use shield")
                {

                }
                //multiway
                else if (input == "use bag of fertiliser on can" || input == "use can on bag of fertiliser")
                {

                }
                else if (input == "use timer on bomb" || input == "use bomb on timer")
                {

                }
            }
            return player;
        }
        //METHODS
        public static void InvalidInput(Player player)
        {
            Console.WriteLine("invalid input, remember to use \"go to\", \"look\", \"open\" and \"use\" correctly");

        }
        //new game
        public static void NewGame(Player player)
        {
            Console.WriteLine("You are now in the " + player._Location._Name);
            GameRound gameRound = new GameRound(player,player._Location,true);
            gameRound.NewInput(player, Console.ReadLine());
        }

        public static void PrintItems(Player player)
        {
            if (player._Location._Items.Count > 0)
            {
                string items = Room.GetCurrentItems(player);
                Console.WriteLine("you open " + player._Location._UsableFurniture._Name + ", inside it there is a " + items);
            }
            else
            {
                Console.WriteLine("you open " + player._Location._UsableFurniture._Name + ", but its empty");
            }
        }
        public static void TakeOrDrop(Player player, string input)
        {
            //take
            if (input.Contains("take"))
            {
                string tempInput = input.Substring(input.IndexOf(' '), input.Length - input.IndexOf(' ')).Trim();
                if (player._Location._Items.Any(x => x._Name == tempInput))
                {
                    var item = player._Location._Items.Where(x => x._Name == tempInput).FirstOrDefault();
                    player._Items.Add(item);
                    player._Location._Items.Remove(item);
                    Console.WriteLine("you take " + item._Name);
                }
                else
                {
                    var item = player._Location._Items.Where(x => x._Name == tempInput).FirstOrDefault();
                    Console.WriteLine("you cant take " + tempInput + " because its not present");
                }
            }
            //drop
            else
            {
                string tempInput = input.Substring(input.IndexOf(' '), input.Length - input.IndexOf(' ')).Trim();
                if (player._Items.Any(x => x._Name == tempInput))
                {
                    var item = player._Items.Where(x => x._Name == tempInput).FirstOrDefault();
                    player._Location._Items.Add(item);
                    player._Items.Remove(item);
                    Console.WriteLine("you put " + item._Name + " in the " + player._Location._UsableFurniture._Name);
                }
                else
                {
                    Console.WriteLine("you cant drop " + tempInput + " because its not in your inventory");
                }
            }
        }
    }
}
