using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.ViewModules
{
    public class FirstViewModel : BaseViewModel
    {
        #region Title
        private string _Title = "DashCode";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
    }
}
