using DashCode.ViewModules;

namespace DashCode.Infrastructure.Services
{
    public class VMLocator
    {
        public MainViewModel MainVM => App.VMService.MainVM;
        public FirstViewModel FirstVM => App.VMService.FirstVM;
        public ChatViewModel ChatVM => App.VMService.ChatVM;
    }
}
