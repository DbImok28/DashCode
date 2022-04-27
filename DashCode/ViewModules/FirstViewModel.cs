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
        private ObservableCollection<ProjectFolder> _Files = new ObservableCollection<ProjectFolder>();
        public ObservableCollection<ProjectFolder> Files
        {
            get => _Files;
            set => Set(ref _Files, value);
        }
        #endregion
        #region RemoveFileCommand
        public ICommand RemoveFileCommand { get; }
        private void OnRemoveFileCommandExecuted(object par)
        {
            if(par is ProjectFolder folder)
            {
                _Files.Remove(folder);
            }
        }
        private bool CanRemoveFileCommandExecute(object par) => Files.Count > 0;
        #endregion
        #region AddFileCommand
        public ICommand AddFileCommand { get; }
        private void OnAddFileCommandExecuted(object par)
        {
            if (par is ProjectFolder folder)
            {
                _Files.Add(folder);
            }
        }
        private bool CanAddFileCommandExecute(object par) => true;
        #endregion

        public FirstViewModel()
        {
            RemoveFileCommand = new LambdaCommand(OnRemoveFileCommandExecuted, CanRemoveFileCommandExecute);
            AddFileCommand = new LambdaCommand(OnAddFileCommandExecuted, CanAddFileCommandExecute);
            try
            {
                Files = FileReader.DeserializeXML<ObservableCollection<ProjectFolder>>("RecentProjectFolders.xml");
            }
            catch (System.Exception)
            {
                FileReader.SerializeToXML("RecentProjectFolders.xml", Files);
            }
            Files.CollectionChanged += (s, a) => FileReader.SerializeToXML("RecentProjectFolders.xml", Files);
        }
    }
}
