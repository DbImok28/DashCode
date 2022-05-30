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
            if (string.IsNullOrEmpty(login.Text))
            {
                MessageBox.Show("The login field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("The password field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(mail.Text))
            {
                MessageBox.Show("The mail field cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
