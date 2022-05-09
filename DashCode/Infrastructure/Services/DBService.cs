using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Linq;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

namespace DashCode.Infrastructure.Services
{
    public class DBService
    {
        private SqlConnection Connection;
        public bool IsConnected = false;
        public DBService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            Task.Run(() => Connect(connectionString));
        }

        public void Connect(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            IsConnected = false;
            try
            {
                Connection.Open();
                IsConnected = true;
            }
            catch (SqlException ex)
            {
                if (File.Exists("DBConnection.txt"))
                {
                    string connectionStr = File.ReadAllText("DBConnection.txt");
                    if (!string.IsNullOrWhiteSpace(connectionStr))
                    {
                        try
                        {
                            Connection = new SqlConnection(connectionStr);
                            Connection.Open();
                            IsConnected = true;
                        }
                        catch (SqlException ex2)
                        {
                            MessageBox.Show(ex2.Message, "DBConnection error");
                        }
                        return;
                    }
                }
                MessageBox.Show(ex.Message, "DBConnection error");
            }
        }
        ~DBService()
        {
            if (IsConnected)
            {
                Connection.Close();
                IsConnected = false;
            }
        }
        public int CallStoredProcedure(string name, params SqlParameter[] parameters)
        {
            try
            {
                //Connection.Open();
                var command = new SqlCommand(name, Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                // return
                var returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnParameter);

                command.ExecuteNonQuery();
                return (int)returnParameter.Value;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }
        public DataSet CallTableStoredProcedure(string name, string outParName, params SqlParameter[] parameters)
        {
            try
            {
                var command = new SqlCommand(name, Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                // XmlOutParameter
                var xmlOutParameter = new SqlParameter($"@{outParName}", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(xmlOutParameter);

                command.ExecuteNonQuery();

                DataSet ds = new DataSet();
                if (xmlOutParameter.Value is string str)
                {
                    ds.ReadXml(new XmlTextReader(new StringReader(str)));
                }
                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
        public bool TryLogin(string login, string password, out byte[] outPhoto)
        {
            if (!IsConnected)
            {
                outPhoto = null;
                return false;
            }

            byte[] photo = null;
            var photoParameter = new SqlParameter("@photo", photo)
            {
                SqlDbType = SqlDbType.VarBinary,
                Size = int.MaxValue,
                Direction = ParameterDirection.Output
            };
            var result = CallStoredProcedure("TRY_LOGIN",
                new SqlParameter("@login", login),
                new SqlParameter("@password", password),
                photoParameter
            );
            if (result > 0)
            {
                //MessageBox.Show("Верно");
                outPhoto = photoParameter.Value as byte[];
                return true;
            }
            else
            {
                outPhoto = null;
                return false;
            }
        }
        public bool Register(string userName, string mail, string password, byte[] photo)
        {
            if (!IsConnected) return false;
            var result = CallStoredProcedure("REGISTER",
                new SqlParameter("@user_name", userName),
                new SqlParameter("@login", mail),
                new SqlParameter("@password", password),
                new SqlParameter("@photo", photo)
            );
            if (result > 0)
            {
                MessageBox.Show("Успех");
                return true;
            }
            else
            {
                MessageBox.Show("Неудача");
                return false;
            }
        }
        public Chat CreateChat(UserAccount user, string name)
        {
            if (!IsConnected) return null;
            int chatId = 0;
            var result = CallStoredProcedure("CREATE_CHAT",
                new SqlParameter("@name", name),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@chat_id", chatId)
                {
                    Direction = ParameterDirection.Output
                }
            );
            if (result > 0)
            {
                return new Chat(chatId, name, new ObservableCollection<User>(), new ObservableCollection<Message>());
            }
            return null;
        }
        public bool DeleteChat(UserAccount user, Chat chat)
        {
            if (!IsConnected) return false;
            var result = CallStoredProcedure("DELETE_CHAT",
                new SqlParameter("@chat_id", chat.Id),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            return result > 0;
        }
        public bool SendMessage(UserAccount user, Chat chat, string msg)
        {
            if (!IsConnected) return false;
            var result = CallStoredProcedure("SEND_MESSAGE",
                new SqlParameter("@chat_id", chat.Id),
                new SqlParameter("@message", msg),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            return result > 0;
        }
        public bool RenameChat(UserAccount user, Chat chat, string name)
        {
            if (!IsConnected) return false;
            var result = CallStoredProcedure("RENAME_CHAT",
                new SqlParameter("@chat_id", chat.Id),
                new SqlParameter("@name", name),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            return result > 0;
        }
        public bool AddUserToChat(UserAccount user, Chat chat, string userName)
        {
            if (!IsConnected) return false;
            var result = CallStoredProcedure("ADD_USER_FOR_CHAT",
                new SqlParameter("@chat_id", chat.Id),
                new SqlParameter("@user_name", userName),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            return result > 0;
        }
        public List<User> LoadUsersForChat(int chatId, UserAccount user)
        {
            if (!IsConnected) return null;
            var users = new List<User>();
            var UsersSet = CallTableStoredProcedure("USERS_FOR_CHAT", "outxml",
                new SqlParameter("@chat_id", chatId),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            if (UsersSet != null && UsersSet.Tables.Count > 0)
            {
                foreach (DataRow UserRow in UsersSet.Tables[0].Rows)
                {
                    byte[] photo = null;
                    if (!UserRow.IsNull("Photo"))
                    {
                        photo = Convert.FromBase64String(UserRow["Photo"].ToString());
                    }
                    users.Add(new User(Convert.ToInt32(UserRow["UserId"].ToString()), UserRow["Name"].ToString(), photo));
                }
            }
            return users;
        }
        public List<Message> LoadMessagesForChat(int chatId, UserAccount user, List<User> users)
        {
            if (!IsConnected) return null;
            var messages = new List<Message>();
            var MessagesSet = CallTableStoredProcedure("MESSAGES_FOR_CHAT", "outxml",
                new SqlParameter("@chat_id", chatId),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
            );
            if (MessagesSet != null && MessagesSet.Tables.Count > 0)
            {
                foreach (DataRow MessageRow in MessagesSet.Tables[0].Rows)
                {
                    messages.Add(new Message(users.First(u => u.Id == Convert.ToInt32(MessageRow["User"].ToString())), MessageRow["Text"].ToString()));
                }
            }
            return messages;
        }
        public List<Chat> LoadChats(UserAccount user)
        {
            if (!IsConnected) return null;
            if (!user.IsValid) return new List<Chat>();

            var result = new List<Chat>();
            var ChatSet = CallTableStoredProcedure("CHATS_FOR_USER", "outxml",
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password)
                );
            if (ChatSet != null && ChatSet.Tables.Count > 0)
            {
                foreach (DataRow ChatRow in ChatSet.Tables[0].Rows)
                {
                    var chatId = Convert.ToInt32(ChatRow[0].ToString());
                    var chatName = ChatRow[1].ToString();
                    var users = LoadUsersForChat(chatId, user);
                    var messages = LoadMessagesForChat(chatId, user, users);

                    var chat = new Chat(chatId, chatName, users, messages);
                    result.Add(chat);
                }
            }
            return result;
        }
    }
}
