using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models
{
    [Serializable]
    public class UserAccount
    {
        public UserAccount()
        {

        }
        public UserAccount(string login, string mail, string password, byte[] image)
        {
            Login = login;
            Mail = mail;
            Password = password;
            BitMap = image;
        }
        public UserAccount(string login, string mail, string password, string imagePath)
        {
            Login = login;
            Mail = mail;
            Password = password;
            BitMap = ImageTools.ImageToByteArr(imagePath);
        }
        public UserAccount(string login, string password, byte[] image)
        {
            Login = login;
            Mail = null;
            Password = password;
            BitMap = image;
        }
        public UserAccount(string login, string password, string imagePath)
        {
            Login = login;
            Mail = null;
            Password = password;
            BitMap = ImageTools.ImageToByteArr(imagePath);
        }
        public string GetAccessString()
        {
            return $"{Login}, {Password}";
        }
        public string Login { get; private set; }
        public string Mail { get; private set; }
        public string Password { get; set; }
        public byte[] BitMap { get; private set; }
        public bool IsValid => Login != null;

        public override string ToString()
        {
            return Login;
        }
    }
}
