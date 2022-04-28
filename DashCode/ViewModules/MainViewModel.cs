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
        private ProjectFolder _CurrentFolder = new ProjectFolder();
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
        #region OpenFile
        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object par)
        {
            CurrentFile = par as ProjectFile;
            FormattedDocument.Open(CurrentFile);
        }
        private bool CanOpenFileCommandExecute(object par) => true;
        #endregion
        #region OpenFolder
        public ICommand SaveFileCommand { get; }
        private void OnSaveFileCommandExecuted(object par) => FormattedDocument.Save();
        private bool CanSaveFileCommandExecute(object par) => true;
        #endregion
        public MainViewModel()
        {
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            OpenFolderCommand = new LambdaCommand(OnOpenFolderCommandExecuted, CanOpenFolderCommandExecute);
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            //            EditorDocument Document = new EditorDocument(@"using DashCode.Models.DocumentParser;
            //using System;
            //using System.Collections.Generic;
            //using System.Collections.ObjectModel;
            //using System.Text;

            //namespace DashCode.Models
            //{ 
            //    public class EditorDocument
            //    {
            //            public EditorDocument(string rawDocument, IDocumentParser parser)
            //            {
            //                if (string.IsNullOrWhiteSpace(rawDocument))
            //                {

            //                }

            //            RawDocument = rawDocument;
            //                Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            //        }

            //        public string _Raw.Document { get; private set; }
            //        public IConstruction ParsedDocument;
            //        public IDocumentParser Parser { get; private set; }
            //        public event EventHandler OnDocumentUpdate;
            //        public FormattedEditorDocument FormattedDocument
            //        {
            //            get => _FormattedDocument;
            //            set => Set(ref _FormattedDocument, value);
            //        }
            //        public FormattedEditorDocument FormattedDocument
            //        {
            //            get => { _FormattedDocument; }
            //            set => Set(ref _FormattedDocument, value);
            //        }
            //        public void AddText(int pos, string str)
            //        {
            //            RawDocument = RawDocument.Insert(pos, str);
            //            OnDocumentUpdate?.Invoke(this, null);
            //        }
            //        public void Parse()
            //        {
            //            Parser.ParseDocument(RawDocument);
            //        }
            //    }
            //}", new CSharpReader());
            EditorDocument Document = new EditorDocument(CurrentFile, new CSharpReader());
            FormattedDocument = new CSharpFormattedDocument(Document);
            Document.Read();
            FormattedDocument.Format();
            OnPropertyChanged("Document");
        }
    }
}

