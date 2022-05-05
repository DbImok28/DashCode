using DashCode.Infrastructure.Commands;
using DashCode.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DashCode.ViewModules
{
    public class ChatViewModel : BaseViewModel
    {
        private ObservableCollection<Chat> _Chats = new ObservableCollection<Chat>()
        {
            new Chat("test", new ObservableCollection<User>()
            {
                new User("vasa"),
                new User("lexa")
            },
            new ObservableCollection<Message>()
            {
                new Message(new User("vasa"), "hello"),
                new Message(new User("lexa"), "hi"),
            }),
            new Chat("test2", new ObservableCollection<User>()
            {
                new User("vasa"),
                new User("lexa"),
                new User("petra"),
            },
            new ObservableCollection<Message>()
            {
                new Message(new User("vasa"), "hello people"),
                new Message(new User("lexa"), "hi"),
                new Message(new User("petra"), "good"),
            })
        };
        public ObservableCollection<Chat> Chats
        {
            get => _Chats;
            set => Set(ref _Chats, value);
        }

        private Chat _SelectedChat;
        public Chat SelectedChat
        {
            get => _SelectedChat;
            set => Set(ref _SelectedChat, value);
        }
        #region SendMessage
        public ICommand SendMessageCommand { get; }
        private void OnSendMessageCommandExecuted(object par) => SendMessage(par as string);
        #endregion
        #region CreateChat
        public ICommand CreateChatCommand { get; }
        private void OnCreateChatCommandExecuted(object par) => CreateChat(par as string);        
        #endregion
        #region DeleteChat
        public ICommand DeleteChatCommand { get; }
        private void OnDeleteChatCommandExecuted(object par) => DeleteChat();     
        #endregion
        #region RenameChat
        public ICommand RenameChatCommand { get; }
        private void OnRenameChatCommandExecuted(object par) => RenameChat(par as string);     
        #endregion
        #region AddUserToChat
        public ICommand AddUserToChatCommand { get; }
        private void OnAddUserToChatCommandExecuted(object par)
        {
            string name = par as string;
            AddUserToChat(name);
        }
        #endregion
        public ChatViewModel()
        {
            SendMessageCommand = new LambdaCommand(OnSendMessageCommandExecuted);
            CreateChatCommand = new LambdaCommand(OnCreateChatCommandExecuted);
            DeleteChatCommand = new LambdaCommand(OnDeleteChatCommandExecuted);
            RenameChatCommand = new LambdaCommand(OnRenameChatCommandExecuted);
            AddUserToChatCommand = new LambdaCommand(OnAddUserToChatCommandExecuted);
            if (Chats.Count > 0)
                SelectedChat = Chats.First();
        }
        public void CreateChat(string name)
        {
            // TODO: Add logic
        }
        public void SendMessage(string msg)
        {
            // TODO: Add logic
        }
        public void DeleteChat()
        {
            // TODO: Add logic
        }
        public void RenameChat(string name)
        {
            // TODO: Add logic

        }
        public void AddUserToChat(string userName)
        {
            // TODO: Add logic
        }
    }
}
