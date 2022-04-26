using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Infrastructure.Services
{
    public class VMService
    {
        public VMService()
        {
            MainWindowVM = new MainWindowViewModel();
        }
        public MainWindowViewModel MainWindowVM { get; private set; }
    }
}
