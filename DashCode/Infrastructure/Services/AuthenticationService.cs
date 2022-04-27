using System;
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
                Account = FileReader.DeserializeBin<UserAccount>("AuthenticationInfo.bin");
                CurrentPage = Autificated;
                State = AuthenticationState.Autificated;
                OnUpdatePage?.Invoke(this, null);
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
            if(IsAutificated)
            {
                FileReader.SerializeToBin("AuthenticationInfo.bin", Account);
            }
        }
        public bool TryLogin(string login, string password)
        {
            string imagePath = ""; // TODO: LoadImage
            SetAutificated(login, null, password, imagePath);
            return true;
        }
        public bool TryRegister(string login, string mail, string password, string imagePath)
        {
            SetAutificated(login, mail, password, imagePath);
            return true;
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
            OnUpdatePage?.Invoke(this, null);
        }
        public void SetNotAutificated()
        {
            CurrentPage = Login;
            State = AuthenticationState.Login;
            Account = null;
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
