using System;
using System.Windows.Controls;
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

       
        public MainViewModel()
        {
            EditorDocument Document = new EditorDocument(@"using DashCode.Models.DocumentParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DashCode.Models
{ 
    public class EditorDocument
    {
            public EditorDocument(string rawDocument, IDocumentParser parser)
            {
                if (string.IsNullOrWhiteSpace(rawDocument))
                {

                }

            RawDocument = rawDocument;
                Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public string _Raw.Document { get; private set; }
        public IConstruction ParsedDocument;
        public IDocumentParser Parser { get; private set; }
        public event EventHandler OnDocumentUpdate;
        public FormattedEditorDocument FormattedDocument
        {
            get => _FormattedDocument;
            set => Set(ref _FormattedDocument, value);
        }
        public FormattedEditorDocument FormattedDocument
        {
            get => { _FormattedDocument; }
            set => Set(ref _FormattedDocument, value);
        }
        public void AddText(int pos, string str)
        {
            RawDocument = RawDocument.Insert(pos, str);
            OnDocumentUpdate?.Invoke(this, null);
        }
        public void Parse()
        {
            Parser.ParseDocument(RawDocument);
        }
    }
}", new CSharpReader());
            FormattedDocument = new CSharpFormattedDocument(Document);
            Document.Read();
            FormattedDocument.Format();
            OnPropertyChanged("Document");
        }
    }
}

