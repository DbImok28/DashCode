using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DashCode.Models
{
    public class Chat : BaseViewModel
    {
        public Chat(string name, ObservableCollection<User> users, ObservableCollection<Message> messages)
        {
            Name = name;
            Users = users;
            Messages = messages;
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
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
        public override string ToString()
        {
            return "Chat";
        }
    }
}
