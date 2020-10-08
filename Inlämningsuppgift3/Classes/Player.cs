using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Player
    {
        public List<Item> _Items { get; set; }
        public Room _Location { get; set; }
        public bool _IsShielded { get; set; }

        public Player()
        {
            _Items = new List<Item>();
            _Location = Room.GetAllRooms().Where(x => x._Name == "hallway").FirstOrDefault();
            _IsShielded = false;
        }
        
    }
}
