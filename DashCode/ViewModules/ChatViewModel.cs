using DashCode.Infrastructure.Commands;
using DashCode.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DashCode.ViewModules
{
    public class ChatViewModel : BaseViewModel
    {
        private ObservableCollection<Chat> _Chats = new ObservableCollection<Chat>();
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
            //App.AuthenticateService.OnUpdatePage += (s, a) => Load();
        }

        public void CreateChat(string name)
        {
            var chat = App.DBService.CreateChat(App.AuthenticateService.Account, name);
            if (chat != null)
            {
                SelectedChat = chat;
                Chats.Add(chat);
                Update();
            }
            else
            {
                MessageBox.Show("Ошибка создания");
            }
        }
        public void SendMessage(string msg)
        {
            if (SelectedChat != null && !App.DBService.SendMessage(App.AuthenticateService.Account, SelectedChat, msg))
            {
                MessageBox.Show("Ошибка отправки сообщения");
            }
            else
            {
                Update();
            }
        }
        public void DeleteChat()
        {
            if (SelectedChat != null && !App.DBService.DeleteChat(App.AuthenticateService.Account, SelectedChat))
            {
                MessageBox.Show("Ошибка удаления");
            }
            else
            {
                Update();
            }
        }
        public void RenameChat(string name)
        {
            if (SelectedChat != null && !App.DBService.RenameChat(App.AuthenticateService.Account, SelectedChat, name))
            {
                MessageBox.Show("Ошибка. Не удалось изменить имя");
            }
            else
            {
                Update();
            }
        }
        public void AddUserToChat(string userName)
        {
            if (SelectedChat != null && !App.DBService.AddUserToChat(App.AuthenticateService.Account, SelectedChat, userName))
            {
                MessageBox.Show("Ошибка добавления пользователя");
            }
            else
            {
                Update();
            }
        }
        public void Update()
        {
            if (App.AuthenticateService.Account != null)
            {
                var chats = App.DBService.LoadChats(App.AuthenticateService.Account);
                if (chats != null)
                {
                    if (chats.Count != Chats.Count)
                    {
                        Chats.Clear();
                        foreach (var chat in chats)
                        {
                            Chats.Add(chat);
                        }
                        if (Chats.Count > 0)
                        {
                            SelectedChat = Chats.First();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < chats.Count; i++)
                        {
                            Chats[i].Update(chats[i]);
                        }
                    }
                }
            }
        }
        public void Load()
        {
            if (App.AuthenticateService.Account != null)
            {
                Chats = new ObservableCollection<Chat>();
                var chats = App.DBService.LoadChats(App.AuthenticateService.Account);
                if (chats != null)
                {
                    Chats.Clear();
                    foreach (var chat in chats)
                    {
                        Chats.Add(chat);
                    }
                    if (Chats.Count > 0)
                    {
                        SelectedChat = Chats.First();
                    }
                }
            }
        }
    }
}
