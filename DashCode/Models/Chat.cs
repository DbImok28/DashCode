using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DashCode.Models
{
    public class Chat : BaseViewModel
    {
        public Chat(int id, string name, ObservableCollection<User> users, ObservableCollection<Message> messages)
        {
            Id = id;
            Name = name;
            Users = users;
            Messages = messages;
        }
        public Chat(int id, string name, List<User> users, List<Message> messages)
        {
            Id = id;
            Name = name;
            Users = new ObservableCollection<User>();
            foreach (var user in users)
            {
                Users.Add(user);
            }

            Messages = new ObservableCollection<Message>();
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        private int _Id;
        public int Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }
        private ObservableCollection<User> _Users;
        public ObservableCollection<User> Users
        {
            get => _Users;
            set => Set(ref _Users, value);
        }
        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }
        public void Update(Chat chat)
        {
            if (Users.Count != chat.Users.Count)
            {
                Users.Clear();
                foreach (var user in chat.Users)
                {
                    Users.Add(user);
                }
            }
            else
            {
                for (int i = 0; i < chat.Users.Count; i++)
                {
                    Users[i].Update(chat.Users[i]);
                }
            }
            if (Messages.Count != chat.Messages.Count)
            {
                Messages.Clear();
                foreach (var message in chat.Messages)
                {
                    Messages.Add(message);
                }
            }
            else
            {
                for (int i = 0; i < chat.Messages.Count; i++)
                {
                    Messages[i].Update(chat.Messages[i]);
                }
            }
            if (Name != chat.Name)
            {
                Name = chat.Name;
            }
        }
        public override string ToString()
        {
            return "Chat";
        }
    }
}
