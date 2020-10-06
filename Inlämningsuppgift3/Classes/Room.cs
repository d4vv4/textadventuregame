using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Room
    {
        public string _Name { get; set; }
        public List<string> _ { get; set; }

        public Room()
        {
            GenerateRoom();
        }

        public Room GenerateRoom()
        {
            Random rand = new Random();
        }
    }
}
