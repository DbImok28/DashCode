using DashCode.Models;
using System.Windows;
using System.Windows.Controls;

namespace DashCode.Views.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(mail.Text)) return;
            string msg;
            if (!Validator.ValidateUserName(login.Text, out msg) || !Validator.ValidateMail(mail.Text, out msg) || !Validator.ValidateRegisterPassword(password.Text, out msg))
            {
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!App.AuthenticateService.TryRegister(login.Text, mail.Text, password.Text, photo.Text))
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
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticateService.SetLogin();
        }

        private void SelectFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    photo.Text = dialog.FileName;
                    photo.UpdateText();
                }
            }
        }
    }
}
