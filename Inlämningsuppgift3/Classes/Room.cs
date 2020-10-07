using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Room
    {
        public string _Name { get; set; }
        public string _Description { get; set; }
        public UsableFurniture _UsableFurniture { get; set; }
        public List<Item> _Items { get; set; }
        public List<Room> _Connected { get; set; }

        public static List<Room> GetAllRooms()
        {
            List<Room> listOfRooms = new List<Room>();

            using (StreamReader sr = new StreamReader(@"rooms.txt"))
            {
                List<string> allLines = sr.ReadToEnd().Split('\n').ToList();
                foreach (string line in allLines)
                {
                    string[] tempstring = line.Split('@');
                    Room room = new Room();
                    room._Name = tempstring[0].Trim();
                    room._Description = tempstring[1].Trim();
                    room._UsableFurniture = new UsableFurniture(tempstring[2].Trim());
                    room._Items = new List<Item>();
                    tempstring[3].Trim();
                    if (tempstring[3].Contains(','))
                    {
                        string[] items = tempstring[3].Split(',');
                        for (int i = 0; i < items.Length; i++)
                        {
                            room._Items.Add(new Item(items[i]));
                        }
                    }
                    else if (tempstring[3].Trim() != "")
                    {
                        room._Items.Add(new Item(tempstring[3]));
                    }

                    listOfRooms.Add(room);
                }
                listOfRooms[0]._Connected = new List<Room> { listOfRooms[1], listOfRooms[2], };
                listOfRooms[1]._Connected = new List<Room> { listOfRooms[0] };
                listOfRooms[2]._Connected = new List<Room> { listOfRooms[0] };
                listOfRooms[3]._Connected = new List<Room> { listOfRooms[0], listOfRooms[4], listOfRooms[5], };
                listOfRooms[4]._Connected = new List<Room> { listOfRooms[3] };
                listOfRooms[5]._Connected = new List<Room> { listOfRooms[3] };
                listOfRooms[6]._Connected = new List<Room> { };
            }
            return listOfRooms;
        }
        // lägg till 3 när upplåst
        public static void AddLivingRoomAsConnected(Room room)
        {
            room._Connected.Add(GetAllRooms()[3]);
        }
        // lägg till 6 när sprängt
        public static void AddHoleAsConnected(Room room)
        {
            room._Connected.Add(GetAllRooms()[6]);
        }
    }
}
