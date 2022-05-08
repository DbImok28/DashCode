using DashCode.ViewModules;

namespace DashCode.Models
{
    public class Message : BaseViewModel
    {
        public Message(User sender, string content)
        {
            Sender = sender;
            Content = content;
        }

        private User _Sender;
        public User Sender
        {
            get => _Sender;
            set => Set(ref _Sender, value);
        }

        private string _Content;
        public string Content
        {
            get => _Content;
            set => Set(ref _Content, value);
        }
        public void Update(Message message)
        {
            Sender = message.Sender;
            Content = message.Content;
        }
        public override string ToString()
        {
            return "Message";
        }
    }
}