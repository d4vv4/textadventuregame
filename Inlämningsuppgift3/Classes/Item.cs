using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Item
    {
        public string _Name { get; set; }

        public Item(string input)
        {
            _Name = input;
        }
        public static void ShowInventory(Player player)
        {
            if (player._Items.Count > 0)
            {
                Console.WriteLine("your current items are:");
                foreach (var item in player._Items)
                {
                    Console.WriteLine(item._Name);
                }
            }
            else
            {
                Console.WriteLine("you dont carry any items yet");
            }
        }
        public static void TakeOrDropOrRemove(Player player, string input)
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
            else if (input.Contains("drop"))
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
            //remove and add new comboItem
            else if (input == "use key on door" || input == "use crowbar on wardrobe")
            {
                string[] tempInput = input.Split(' ');
                var item = player._Items.Where(x => x._Name == tempInput[1]).FirstOrDefault();
                player._Items.Remove(item);
            }
            else if (input == "use bag of fertiliser on can" || input == "use can on bag of fertiliser")
            {
                if (player._Items.Any(x => x._Name == "bag of fertiliser") && player._Items.Any(x => x._Name == "can"))
                {
                    var tempItem1 = player._Items.Where(x => x._Name == "bag of fertiliser").First();
                    var tempItem2 = player._Items.Where(x => x._Name == "can").First();
                    player._Items.Remove(tempItem1);
                    player._Items.Remove(tempItem2);
                    //add bomb
                    player._Items.Add(new Item("bomb"));
                }
                else
                {
                    Console.WriteLine("you dont posses both bag of fertiliser and can");
                }
            }
            else if (input == "use timer on bomb" || input == "use bomb on timer")
            {
                if (player._Items.Any(x => x._Name == "timer") && player._Items.Any(x => x._Name == "bomb"))
                {
                    var tempItem1 = player._Items.Where(x => x._Name == "timer").First();
                    var tempItem2 = player._Items.Where(x => x._Name == "bomb").First();
                    player._Items.Remove(tempItem1);
                    player._Items.Remove(tempItem2);
                    //add 
                    player._Items.Add(new Item("timed bomb"));
                }
                else
                {
                    Console.WriteLine("you dont posses both timer and bomb");
                }
            }
            else if (input == "use timed timer on crack")
            {
                var tempItem = player._Items.Where(x => x._Name == "timed bomb").First();
                player._Items.Remove(tempItem);
            }
        }
        
    }
}
