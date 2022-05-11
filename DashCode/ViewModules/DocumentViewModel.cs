using DashCode.Infrastructure.Commands;
using DashCode.Models;
using DashCode.Models.Document;
using System.Windows.Input;

namespace DashCode.ViewModules
{
    public class DocumentViewModel : BaseViewModel
    {
        #region CurrentFile
        private ProjectFile _CurrentFile = new ProjectFile();
        public ProjectFile CurrentFile
        {
            get => _CurrentFile;
            set => Set(ref _CurrentFile, value);
        }
        #endregion
        #region Document
        public FormattedStrings FormattedDocument => Document.FormattedDocument;

        private EditorDocument _Document;
        public EditorDocument Document
        {
            get => _Document;
            private set => Set(ref _Document, value);
        }
        #endregion
        #region UpdateDocument
        public ICommand UpdateDocumentCommand { get; }
        private void OnUpdateDocumentCommandExecuted(object par)
        {
            if (par is string content)
            {
                Document.SetText(content);
            }
            Document.ReadAndFormat();
            OnPropertyChanged(nameof(Document));
        }
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
            Document.Save();
            OnPropertyChanged(nameof(CurrentFile));
        }
        private bool CanSaveFileCommandExecute(object par) => true;
        #endregion
        public void Update(string content)
        {
            Document.SetText(content);
            Document.ReadAndFormat();
            OnPropertyChanged(nameof(Document));
        }

        public void Open(ProjectFile file)
        {
            Document.Open(file);
        }
        public void TrySelectNewFile(ProjectFile file)
        {
            CurrentFile = file;
            Document.Open(CurrentFile);
        }

        public DocumentViewModel()
        {
            UpdateDocumentCommand = new LambdaCommand(OnUpdateDocumentCommandExecuted);
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);

            Document = new EditorDocument(null);
            Document.ReadAndFormat();
        }
    }
}