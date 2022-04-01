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
using DashCode.Models.Parsers.CSharp;

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
        #region Document
        private EditorDocument _Document;
        public EditorDocument Document
        {
            get => _Document;
            set => Set(ref _Document, value);
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

        public void AddText(int pos, string str)
        {
            Document.AddText(pos, str);
        }
        public void RemoveText(int pos, int length)
        {
            Document.RemoveText(pos, length);
        }


       
        public MainWindowViewModel()
        {
            Document = new EditorDocument("namespace Hay{public class   Hello{}}}", new CSharpDocumentParser());
            FormattedDocument = new FormattedEditorDocument(Document);
            //Document = new EditorDocument("int Count;\n double Value;", new CSharpDocumentParser());
            //Document = new EditorDocument("namespace Hay{public class Hello    {public string msg{get; set;} public void Say(){}}}", new CSharpDocumentParser());
            //Document.Parse();
        }
    }
}
