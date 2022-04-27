using DashCode.Models;
using DashCode.Infrastructure.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        private ObservableCollection<ProjectFolder> _Files = new ObservableCollection<ProjectFolder>() { new ProjectFolder(@"E:\VSProjects\DashCode\DashCode"), new ProjectFolder(@"E:\VSProjects\Math") };
        public ObservableCollection<ProjectFolder> Files
        {
            get => _Files;
            set => Set(ref _Files, value);
        }
        #endregion
        #region OpenFileCommand
        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object par)
        {

        }
        private bool CanOpenFileCommandExecute(object par) => System.IO.File.Exists((par as ProjectFolder)?.Path);
        #endregion

        public FirstViewModel()
        {
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
        }
    }
}
