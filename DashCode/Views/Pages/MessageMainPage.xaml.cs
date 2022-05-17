using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DashCode.Views.Pages
{
    /// <summary>
    /// Interaction logic for MessageMainPage.xaml
    /// </summary>
    public partial class MessageMainPage : Page
    {
        NoConnectionPage _NoConnectionPage = null;
        public MessageMainPage()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += UpdateTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (App.DBService.IsConnected)
            {
                if (_NoConnectionPage != null)
                {
                    if (_NoConnectionPage.NavigationService.CanGoBack)
                    {
                        _NoConnectionPage.NavigationService.GoBack();
                    }
                    _NoConnectionPage = null;
                }
                App.VMService.ChatVM.Update();
            }
            else
            {
                if (_NoConnectionPage == null)
                {
                    _NoConnectionPage = new NoConnectionPage();
                    NavigationService?.Navigate(_NoConnectionPage);
                }
            }
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"\Views\Pages\CreateChatPage.xaml", UriKind.Relative));
        }

        private void EditChatButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"\Views\Pages\EditChatPage.xaml", UriKind.Relative));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.DBService.IsConnected)
            {
                var task = Task.Run(
                    () =>
                    {
                        App.DBService.Connect();
                        if (App.DBService.IsConnected)
                        {
                            App.VMService.ChatVM.Update();
                        }
                    }
                );
            }
            else
            {
                App.VMService.ChatVM.Update();
            }
        }
    }
}
