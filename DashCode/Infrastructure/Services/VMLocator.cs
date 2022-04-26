using DashCode.ViewModules;

namespace DashCode.Infrastructure.Services
{
    public class VMLocator
    {
        public MainWindowViewModel MainWindowModel => App.VMService.MainWindowVM;
    }
}
