using DashCode.Models;
using System.Windows;
using System.Windows.Controls;

namespace DashCode.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login.Text) || string.IsNullOrEmpty(password.Text)) return;
            if (!Validator.ValidateLogin(login.Text, out string msg))
            {
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!App.AuthenticateService.TryLogin(login.Text, password.Text))
            {
                if (App.DBService.IsConnected)
                {
                    MessageBox.Show("Wrong login or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("No connection to server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticateService.SetRegister();
        }
    }
}
