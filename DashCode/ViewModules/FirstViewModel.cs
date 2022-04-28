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
        #region RemoveFolderCommand
        public ICommand RemoveFolderCommand { get; }
        private void OnRemoveFolderCommandExecuted(object par)
        {
            if(par is ProjectFolder folder)
            {
                _Files.Remove(folder);
            }
        }
        private bool CanRemoveFolderCommandExecute(object par) => Files.Count > 0;
        #endregion
        #region AddFolderCommand
        public ICommand AddFolderCommand { get; }
        private void OnAddFolderCommandExecuted(object par)
        {
            if (par is ProjectFolder folder)
            {
                _Files.Add(folder);
            }
        }
        private bool CanAddFolderCommandExecute(object par) => true;
        #endregion

        public FirstViewModel()
        {
            RemoveFolderCommand = new LambdaCommand(OnRemoveFolderCommandExecuted, CanRemoveFolderCommandExecute);
            AddFolderCommand = new LambdaCommand(OnAddFolderCommandExecuted, CanAddFolderCommandExecute);
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
