using System.Windows.Controls;
using System.Windows.Input;
using DashCode.Infrastructure.Commands;
using DashCode.Models;
using DashCode.Models.Document;

namespace DashCode.ViewModules
{
    public class MainViewModel : BaseViewModel
    {
        #region Title
        private string _Title = "Filename - DashCode";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
        #region FontSize
        private int _FontSize = 14;
        public int FontSize
        {
            get => _FontSize;
            set => Set(ref _FontSize, value);
        }
        #endregion     
        
        #region CurrentDocument
        private DocumentViewModel _CurrentDocument = new DocumentViewModel();
        public DocumentViewModel CurrentDocument
        {
            get => _CurrentDocument;
            set => Set(ref _CurrentDocument, value);
        }
        #endregion

        #region CurrentFolder
        private ProjectFolder _CurrentFolder = new ProjectFolder("C:\\");
        public ProjectFolder CurrentFolder
        {
            get => _CurrentFolder;
            set => Set(ref _CurrentFolder, value);
        }
        #endregion
        #region OpenFolder
        public ICommand OpenFolderCommand { get; }
        private void OnOpenFolderCommandExecuted(object par) => CurrentFolder = par as ProjectFolder;
        private bool CanOpenFolderCommandExecute(object par) => true;
        #endregion

        public MainViewModel()
        {
            OpenFolderCommand = new LambdaCommand(OnOpenFolderCommandExecuted, CanOpenFolderCommandExecute);
        }
    }
}

