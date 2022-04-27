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
    }
}
