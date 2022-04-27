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

        private void SignOut_Button_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticateService.SetNotAutificated();
        }
    }
}
