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
            ImagePath = null;
            BitMap = image;
        }
        public UserAccount(string login, string mail, string password, string imagePath)
        {
            Login = login;
            Mail = mail;
            Password = password;
            ImagePath = imagePath;
            BitMap = GetBitMap(imagePath);
        }
        public UserAccount(string login, string password, byte[] image)
        {
            Login = login;
            Mail = null;
            Password = password;
            ImagePath = null;
            BitMap = image;
        }
        public UserAccount(string login, string password, string imagePath)
        {
            Login = login;
            Mail = null;
            Password = password;
            ImagePath = imagePath;
            BitMap = GetBitMap(imagePath);
        }
        public byte[] GetBitMap(string imagePath)
        {
            return null;
        }
        public string GetAccessString()
        {
            return $"{Login}, {Password}";
        }
        public string Login { get; private set; }
        public string Mail { get; private set; }
        private string Password { get; set; }
        public string ImagePath { get; private set; }
        public byte[] BitMap { get; private set; }

        public override string ToString()
        {
            return Login;
        }
    }
}
