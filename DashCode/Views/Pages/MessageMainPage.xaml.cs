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
    /// Interaction logic for MessageMainPage.xaml
    /// </summary>
    public partial class MessageMainPage : Page
    {
        public MessageMainPage()
        {
            InitializeComponent();
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"\Views\Pages\CreateChatPage.xaml", UriKind.Relative));
        }

        private void EditChatButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"\Views\Pages\EditChatPage.xaml", UriKind.Relative));
        }
    }
}
