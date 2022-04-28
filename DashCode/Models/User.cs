namespace DashCode.Models
{
    public class User
    {
        public User(string name, byte[] icon = null)
        {
            Name = name;
            Icon = icon;
        }

        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}