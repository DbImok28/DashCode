using System;
using System.Windows.Controls;
using System.Windows.Input;
using DashCode.Infrastructure.Commands;
using DashCode.Models;
using DashCode.Models.CSharpReader;

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
        #region LastTextChange
        private TextChange _LastTextChange;
        public TextChange LastTextChange
        {
            get => _LastTextChange;
            set => Set(ref _LastTextChange, value);
        }
        #endregion
        #region FormattedDocument
        private FormattedEditorDocument _FormattedDocument;
        public FormattedEditorDocument FormattedDocument
        {
            get => _FormattedDocument;
            set => Set(ref _FormattedDocument, value);
        }
        #endregion
        #region CurrentFile
        private ProjectFile _CurrentFile = new ProjectFile();
        public ProjectFile CurrentFile
        {
            get => _CurrentFile;
            set => Set(ref _CurrentFile, value);
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
        //public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");

        //#region SelectedDirectory : DirectoryViewModel - Выбранная директория

        //private DirectoryViewModel _SelectedDirectory;
        //public DirectoryViewModel SelectedDirectory { get => _SelectedDirectory; set => Set(ref _SelectedDirectory, value); }
        #region OpenFolder
        public ICommand OpenFolderCommand { get; }
        private void OnOpenFolderCommandExecuted(object par) => CurrentFolder = par as ProjectFolder;
        private bool CanOpenFolderCommandExecute(object par) => true;
        #endregion
        #region OpenFile
        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object par) => TrySelectNewFile(par as ProjectFile);
        private bool CanOpenFileCommandExecute(object par) => true;
        #endregion
        #region SaveFile
        public ICommand SaveFileCommand { get; }
        private void OnSaveFileCommandExecuted(object par)
        {
            FormattedDocument.Save();
            OnPropertyChanged(nameof(CurrentFile));
        }
        private bool CanSaveFileCommandExecute(object par) => true;
        #endregion
        public void TrySelectNewFile(ProjectFile file)
        {
            CurrentFile = file;
            FormattedDocument.Open(CurrentFile);
        }
        public MainViewModel()
        {
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            OpenFolderCommand = new LambdaCommand(OnOpenFolderCommandExecuted, CanOpenFolderCommandExecute);
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            EditorDocument Document = new EditorDocument(CurrentFile, new CSharpReader());
            FormattedDocument = new CSharpFormattedDocument(Document);
            Document.Read();
            FormattedDocument.Format();
            OnPropertyChanged("Document");
        }
    }
}

