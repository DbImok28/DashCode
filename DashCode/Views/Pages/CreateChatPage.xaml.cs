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
            if(!string.IsNullOrEmpty(chatName.Text))
            {
                App.VMService.ChatVM.CreateChatCommand.Execute(chatName.Text);
                //NavigationService.Navigate(new Uri(@"\Views\Pages\MessageMainPage.xaml", UriKind.Relative));
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
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
