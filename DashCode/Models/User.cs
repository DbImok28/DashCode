using DashCode.ViewModules;

namespace DashCode.Models
{
    public class User : BaseViewModel
    {
        public User(int id, string name, byte[] icon = null)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
        private int _Id;
        public int Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        private byte[] _Icon;
        public byte[] Icon
        {
            get => _Icon;
            set => Set(ref _Icon, value);
        }
        public void Update(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Icon = user.Icon;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}