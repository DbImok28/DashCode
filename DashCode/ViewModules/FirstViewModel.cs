using DashCode.Models;
using DashCode.Infrastructure.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

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
        #region Files
        private ObservableCollection<ProjectFolder> _Folders = new ObservableCollection<ProjectFolder>();
        public ObservableCollection<ProjectFolder> Folders
        {
            get => _Folders;
            set => Set(ref _Folders, value);
        }
        #endregion
        #region RemoveFolderCommand
        public ICommand RemoveFolderCommand { get; }
        private void OnRemoveFolderCommandExecuted(object par)
        {
            if(par is ProjectFolder folder)
            {
                _Folders.Remove(folder);
            }
        }
        private bool CanRemoveFolderCommandExecute(object par) => Folders.Count > 0;
        #endregion
        #region AddFolderCommand
        public ICommand AddFolderCommand { get; }
        private void OnAddFolderCommandExecuted(object par)
        {
            if (par is ProjectFolder folder)
            {
                _Folders.Add(folder);
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
                var FilesPathies = FileReader.DeserializeXML<ObservableCollection<string>>("RecentProjectFolders.xml");
                foreach (var path in FilesPathies)
                {
                    Folders.Add(new ProjectFolder(path));
                }
            }
            catch (System.Exception)
            {
                FileReader.SerializeToXML("RecentProjectFolders.xml", Folders.Select(x => x.Path).ToList());
            }
            Folders.CollectionChanged += (s, a) => FileReader.SerializeToXML("RecentProjectFolders.xml", Folders.Select(x => x.Path).ToList());
        }
    }
}
