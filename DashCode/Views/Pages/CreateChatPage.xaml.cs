using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DashCode.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateChatPage.xaml
    /// </summary>
    public partial class CreateChatPage : Page
    {
        public CreateChatPage()
        {
            InitializeComponent();
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(chatName.Text)) return;
            string msg;
            if (!Validator.ValidateName(chatName.Text, out msg))
            {
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            App.VMService.ChatVM.CreateChatCommand.Execute(chatName.Text);

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
