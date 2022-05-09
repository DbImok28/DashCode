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
            if (!App.AuthenticateService.TryLogin(login.Text, password.Text))
            {
                if (App.DBService.IsConnected)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Нет соединения с сервером", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticateService.SetRegister();
        }
    }
}
