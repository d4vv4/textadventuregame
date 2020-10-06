using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Item
    {
        public string _Name { get; set; }

        public Item(string item)
        {
            _Name = item;
        }
        


        //public static List<Item> getAllItems()
        //{
        //    List<Item> listOfItems = new List<Item>();

        //    using (StreamReader sr = new StreamReader(@"items.txt"))
        //    {
        //        List<string> allLines = sr.ReadToEnd().Split('\n').ToList();
        //        foreach (string line in allLines)
        //        {
        //            Item item = new Item(line.Trim());
        //            listOfItems.Add(item);
        //        }
        //    }
        //    return listOfItems;
        //}
        
    }
}
