using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (App.AuthenticateService.TryRegister(login.Text, mail.Text, password.Text, photo.Text))
            {

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
