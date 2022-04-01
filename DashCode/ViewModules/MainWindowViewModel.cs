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
        
        #region EditorDocument
        private FlowDocument _EditorDocument;
        public event EventHandler OnEditorDocumentChenged;
        public FlowDocument EditorDocument
        {
            get => _EditorDocument;
            set
            {
                _EditorDocument = value;
                Set(ref _EditorDocument, value);
            }
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

        public void AddText(int pos, string str)
        {
            Document.AddText(pos, str);
        }
        public void RemoveText(int pos, int length)
        {
            Document.RemoveText(pos, length);
        }


        public FlowDocument ConvertToDocument(FormattedStrings formattedStrings)
        {
            FlowDocument document = new FlowDocument();
            Paragraph currentParagraph = new Paragraph();
            foreach (var str in formattedStrings.Strings)
            {
                if (str.Text == "\n")
                {
                    document.Blocks.Add(currentParagraph);
                    currentParagraph = new Paragraph();
                }
                else
                {
                    var brush = new SolidColorBrush(str.TextColor);
                    var run = new Run(str.Text);
                    run.Foreground = brush;
                    currentParagraph.Inlines.Add(run);
                }
            }
            document.Blocks.Add(currentParagraph);
            return document;
        }
        public MainWindowViewModel()
        {
            Document = new EditorDocument("int Count;\n double Value;", new CSharpDocumentParser());
            Document.Parse();
            EditorDocument = ConvertToDocument(Document.FormattedDocument);
        }
    }
}
