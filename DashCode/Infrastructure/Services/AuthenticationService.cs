using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DashCode.Models;
using DashCode.Views.Pages;

namespace DashCode.Infrastructure.Services
{
    public enum AuthenticationState
    {
        Login,
        Register,
        Autificated
    }
    public class AuthenticationService
    {
        public AuthenticationService()
        {
            Login = new LoginPage();
            Register = new RegisterPage();
            Autificated = new AutificatedPage();
            OnUpdatePage += (s, a) => Autificated.loginBlock.Text = AccountLogin;
            try
            {
                if (File.Exists("AuthenticationInfo.bin"))
                {
                    Account = FileReader.DeserializeBin<UserAccount>("AuthenticationInfo.bin");
                    CurrentPage = Autificated;
                    State = AuthenticationState.Autificated;
                    OnUpdatePage?.Invoke(this, null);
                }
                else
                {
                    Account = null;
                    CurrentPage = Login;
                    State = AuthenticationState.Login;
                    OnUpdatePage?.Invoke(this, null);
                }
            }
            catch (Exception)
            {
                Account = null;
                CurrentPage = Login;
                State = AuthenticationState.Login;
                OnUpdatePage?.Invoke(this, null);
            }
        }
        ~AuthenticationService()
        {
            if (IsAutificated)
            {
                FileReader.SerializeToBin("AuthenticationInfo.bin", Account);
            }
        }
        public bool TryLogin(string login, string password)
        {
            if (App.DBService.TryLogin(login, password, out byte[] photo))
            {
                SetAutificated(login, null, password, photo);
                return true;
            }
            return false;
        }
        
        public bool TryRegister(string login, string mail, string password, string imagePath)
        {
            var photo = ImageTools.ImageToByteArr(imagePath);
            if (App.DBService.Register(login, mail, password, photo))
            {
                SetAutificated(login, mail, password, photo);
                return true;
            }
            return false;
        }
        public void SignOut()
        {
            SetNotAutificated();
        }
        public void SetRegister()
        {
            Account = null;
            CurrentPage = Register;
            State = AuthenticationState.Register;
            OnUpdatePage?.Invoke(this, null);
        }
        public void SetLogin()
        {
            SetNotAutificated();
        }
        public void SetAutificated(string login, string mail, string password, string imagePath)
        {
            Account = new UserAccount(login, mail, password, imagePath);
            CurrentPage = Autificated;
            State = AuthenticationState.Autificated;
            FileReader.SerializeToBin("AuthenticationInfo.bin", Account);
            OnUpdatePage?.Invoke(this, null);
        }
        public void SetAutificated(string login, string mail, string password, byte[] photo)
        {
            Account = new UserAccount(login, mail, password, photo);
            CurrentPage = Autificated;
            State = AuthenticationState.Autificated;
            FileReader.SerializeToBin("AuthenticationInfo.bin", Account);
            OnUpdatePage?.Invoke(this, null);
        }
        public void SetNotAutificated()
        {
            CurrentPage = Login;
            State = AuthenticationState.Login;
            Account = null;
            File.Delete("AuthenticationInfo.bin");
            OnUpdatePage?.Invoke(this, null);
        }
        public event EventHandler OnUpdatePage;
        public AuthenticationState State { get; private set; }
        public Page CurrentPage { get; private set; }
        public LoginPage Login { get; }
        public RegisterPage Register { get; }
        public AutificatedPage Autificated { get; }
        public UserAccount Account { get; private set; }
        public string AccountLogin => Account?.Login ?? "";
        public bool IsAutificated => Account != null;
    }
}
