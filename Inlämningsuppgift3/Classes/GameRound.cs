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
        public bool _WardrobeIsLocked { get; set; }
        public bool _CrackIsLocked { get; set; }
        public bool _DoorIsOpen { get; set; }
        public GameRound()
        {
            _DoorIsLocked = true;
            _WardrobeIsLocked = true;
            _CrackIsLocked = true;
            _DoorIsOpen = false;
        }

        List<string> possibleMovementInputs = new List<string> {"go to hallway", "go to kitchen", "go to living room", "go to storage room",
                                                                "go to bedroom", "go to restroom", "go thru hole"};

        List<string> possibleUseInputs = new List<string> { "use key on door", "use crowbar on wardrobe", "use shield", "use bag of fertiliser on can",
                                                            "use timer on bomb","use bomb on timer", "use can on bag of fertiliser", "use timed bomb on crack" };

        List<string> possibleInspects = new List<string> { "inspect door", "inspect crack" };
        public void NewInput(Player player, string input)
        {
            //if input is look
            if (input == "look")
            {
                Console.WriteLine(player._Location._Description);
            }
            //if input is items
            else if (input == "items")
            {
                Item.ShowInventory(player);
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
                    if (player._Location._Name == "hallway" && tempInput == "living room" && (_DoorIsLocked || !_DoorIsOpen))
                    {
                        Console.WriteLine("door is closed or locked, try to open or unlock the door");
                    }
                    else if (player._Location._Name == "living room" && tempInput == "hole")
                    {
                        if (_CrackIsLocked)
                        {
                            Console.WriteLine("you cant go thru the crack, its too small, you need to somehow make a hole in the wall");
                        }
                        else
                        {
                            Console.WriteLine("you walk out the hole and realize you made it out of the house, game is now complete." +
                                "\nwant to play again?");
                            RestartGame();
                        }
                    }
                    else
                    {
                        player._Location = connectedRooms.Where(x => x._Name == tempInput).FirstOrDefault();
                        Console.WriteLine("you are now in the " + player._Location._Name);
                    }
                }
                else if (player._Location._Name == tempInput)
                {
                    Console.WriteLine("invalid input, you are already in " + player._Location._Name);
                }
                else
                {
                    Console.WriteLine("Invalid input, room " + player._Location._Name + " and " + tempInput + " is not connected");
                }
            }
            //if input is about opening usableFurniture
            else if (input == "open " + player._Location._UsableFurniture._Name)
            {
                //special for door and wardrobe since locked

                if (player._Location._Name == "hallway")
                {
                    if (_DoorIsLocked)
                    {
                        Console.WriteLine("the door is locked");
                    }
                    else
                    {
                        _DoorIsOpen = true;
                        Console.WriteLine("you open the door and are now able to go to living room");
                    }
                }
                else if (player._Location._Name == "bedroom")
                {
                    if (_WardrobeIsLocked)
                    {
                        Console.WriteLine("the wardrobe is locked and the lock is broken, maybe you can force it open with a tool?");
                    }
                    else
                    {
                        player._Location._UsableFurniture._IsOpen = true;
                        Room.PrintItems(player);
                    }
                }
                else
                {
                    player._Location._UsableFurniture._IsOpen = true;
                    Room.PrintItems(player);
                }
            }
            //take/drop input
            else if (input.Contains("take") || input.Contains("drop"))
            {
                try
                {
                    if (player._Location._UsableFurniture._IsOpen == true)
                    {
                        Item.TakeOrDropOrRemove(player, input);
                    }
                    else
                    {
                        InvalidInput(player);
                    }

                }
                catch (Exception)
                {
                    InvalidInput(player);
                }
            }
            //possible combination and uses of items
            else if (possibleUseInputs.Contains(input))
            {
                //single way uses
                if (input == "use key on door" && player._Location._Name == "hallway")
                {
                    if (player._Items.Any(x => x._Name == "key"))
                    {
                        Item.TakeOrDropOrRemove(player, input);
                        _DoorIsLocked = false;
                        Console.WriteLine("you unlocked the door");
                    }
                    else
                    {
                        Console.WriteLine("you need a key to unlock the door");
                    }
                }
                else if (input == "use crowbar on wardrobe" && player._Location._Name == "bedroom")
                {
                    if (player._Items.Any(x => x._Name == "crowbar"))
                    {
                        Item.TakeOrDropOrRemove(player, input);
                        _WardrobeIsLocked = false;
                        Console.WriteLine("you break open the wardrobe and are now able to open it");
                    }
                    else
                    {
                        Console.WriteLine("you dont have a crowbar in your inventory");
                    }
                }
                else if (input == "use shield")
                {
                    if (player._Items.Any(x => x._Name == "shield"))
                    {
                        Console.WriteLine("you raise the shield, and are now protected");
                        player._IsShielded = true;
                    }
                }
                else if (input == "use timed bomb on crack" && player._Location._Name == "living room")
                {
                    if (player._Items.Any(x => x._Name == "timed bomb"))
                    {
                        if (player._IsShielded)
                        {
                            _CrackIsLocked = false;
                            Console.WriteLine("you blew a whole in the wall, and are now able to go thru hole");
                        }
                        else
                        {
                            Console.WriteLine("you died of the explosion, game over.. want to try again?");
                            RestartGame();
                        }
                        Item.TakeOrDropOrRemove(player, input);
                    }
                    else
                    {
                        Console.WriteLine("you dont have a timed bomb in your inventory");
                    }
                }
                //multiway
                else if (input == "use bag of fertiliser on can" || input == "use can on bag of fertiliser")
                {
                    Item.TakeOrDropOrRemove(player, input);
                    Console.WriteLine("you combined the items to a bomb");
                }
                else if (input == "use timer on bomb" || input == "use bomb on timer")
                {
                    Item.TakeOrDropOrRemove(player, input);
                    Console.WriteLine("you combined the items to a timed bomb");
                }
            }
            //else
            else
            {
                InvalidInput(player);
            }
            NewInput(player, Console.ReadLine().ToLower());
        }
        //METHODS
        public static void InvalidInput(Player player)
        {
            Console.WriteLine("invalid input, remember to use \"go to\", \"look\", \"open\" and \"use\" correctly");
        }
        //new game
        public static void NewGame(Player player)
        {
            GameRound gameRound = new GameRound();
            Console.WriteLine("You are now in the " + player._Location._Name);
            gameRound.NewInput(player, Console.ReadLine());
        }
        //restart game
        public static void RestartGame()
        {
            if (Console.ReadLine().ToLower() == "yes")
            {
                Console.Clear();
                textadventuregame.WelcomeMSG();
                NewGame(new Player());
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
