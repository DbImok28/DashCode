using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DashCode.Models;
using DashCode.Models.CSharpReader;
using DashCode.Models.DataBase;

namespace DashCode.ViewModules
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Title
        private string _Title = "DashCode";
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

       
        public MainWindowViewModel()
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

            using (var context = new DashCodeBDContext())
            {
                //DESKTOP-2EFHGBU
                var users = context.Users.ToList();
            }
        }
    }
}

