using DashCode.Infrastructure.Services;
using System.Windows;

namespace DashCode
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static VMService VMService { get; } = new VMService();
        public static AuthenticationService AuthenticateService { get; } = new AuthenticationService();
        public static DBService DBService { get; } = new DBService();
        public static void DragWindow()
        {
            Current.MainWindow.DragMove();
        }
        public static void MinimizeWindow()
        {
            Current.MainWindow.WindowState = WindowState.Minimized;
        }
        public static void CloseWindow()
        {
            Current.Shutdown();
        }
        public static void RestoreWindow()
        {
            if (Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Current.MainWindow.WindowState = WindowState.Normal;
            }
            else if (Current.MainWindow.WindowState == WindowState.Normal)
            {
                Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }

}
