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
            MainVM = new MainViewModel();
            FirstVM = new FirstViewModel();
        }
        public MainViewModel MainVM { get; private set; }
        public FirstViewModel FirstVM { get; private set; }
    }
}
