namespace Inlämningsuppgift3.Classes
{
    public class UsableFurniture
    {
        public string _Name { get; set; }
        public bool _IsOpen { get; set; }

        public UsableFurniture(string input)
        {
            _Name = input;
            _IsOpen = false;
        }
    }
}
