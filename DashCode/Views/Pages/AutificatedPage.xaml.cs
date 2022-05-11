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
    /// Interaction logic for AutificatedPage.xaml
    /// </summary>
    public partial class AutificatedPage : Page
    {
        public AutificatedPage()
        {
            InitializeComponent();
        }
        public void Update(UserAccount account)
        {
            if (account != null)
            {
                var photo = account.BitMap;
                if (photo != null)
                {
                    try
                    {
                        UserIcon.Source = ImageTools.ByteArrToBitmapImage(photo);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Load image error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    UserIcon.Source = ImageTools.LoadBitmapFromResource("/Resources/user_no_login.png");
                }
                loginBlock.Text = account.Login;
            }
        }
        private void SignOut_Button_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticateService.SetNotAutificated();
        }
    }
}
