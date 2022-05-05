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
                NavigationService.Navigate(new Uri(@"\Views\Pages\MessageMainPage.xaml", UriKind.Relative));
            }
        }
    }
}
