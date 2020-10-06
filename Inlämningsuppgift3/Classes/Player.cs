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
        public bool _HasFiguredBomb { get; set; }

        List<string> possibleMovementInputs = new List<string> { "south", "west", "north", "east" };

        public Player(Room location)
        {
            _Location = location;
            _HasFiguredBomb = false;
        }
        public Player NewInput(Player player, string input)
        {
            if (possibleMovementInputs.Contains(input))
            {
                var movement = possibleMovementInputs.Where
                    (x => x.Contains(input));
                Console.WriteLine(movement.FirstOrDefault());
            }
            return player;
        }
    }
}
