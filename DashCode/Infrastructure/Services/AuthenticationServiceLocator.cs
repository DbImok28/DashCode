using DashCode.Models;
using DashCode.ViewModules;

namespace DashCode.Infrastructure.Services
{
    public class AuthenticationServiceLocator
    {
        public UserAccount Account => App.AuthenticateService.Account;
        public string Login => App.AuthenticateService?.Account?.Login ?? "-";
        public AuthenticationService Service => App.AuthenticateService;
    }
}
