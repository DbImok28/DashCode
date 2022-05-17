using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DashCode.Views.Pages
{
    /// <summary>
    /// Interaction logic for NoConnectionPage.xaml
    /// </summary>
    public partial class NoConnectionPage : Page
    {
        public NoConnectionPage()
        {
            InitializeComponent();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.DBService.IsConnected)
            {
                var task = Task.Run(
                    () =>
                    {
                        App.DBService.Connect();
                    }
                );
            }
            else
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
