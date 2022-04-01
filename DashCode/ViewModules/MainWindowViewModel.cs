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
        #region EditorTexts
        private ObservableCollection<FormattedString> _EditorTexts;
        public ObservableCollection<FormattedString> EditorTexts
        {
            get => _EditorTexts;
            set => Set(ref _EditorTexts, value);
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

        public string Document { get; set; }

        public void AddText(int pos, string str)
        {
            Document.Insert(pos, str);
        }
        public void RemoveText(int pos, int length)
        {
            //Document.Remove(pos, length);
        }


        public FlowDocument ConvertToDocument(IEnumerable<FormattedString> formattedString)
        {
            FlowDocument document = new FlowDocument();
            Paragraph currentParagraph = new Paragraph();
            foreach (var str in formattedString)
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
            Color keyNamesColor = Color.FromRgb(0,0,255);
            Color variableColor = Color.FromRgb(191, 141, 80);
            Color defaultColor = Color.FromRgb(0,0,0);

            Document = "int Count;\n double Value;";
            // _EditorTexts = Parser.GetFormattedStrings(Document);

            _EditorTexts = new ObservableCollection<FormattedString>{
                new FormattedString("int", keyNamesColor),
                new FormattedString(" ", defaultColor),
                new FormattedString("Count", variableColor),
                new FormattedString(";", defaultColor),
                new FormattedString("\n", defaultColor),
                new FormattedString("double", keyNamesColor),
                new FormattedString(" ", defaultColor),
                new FormattedString("Value", variableColor),
                new FormattedString(";", defaultColor)
            };
            EditorDocument = ConvertToDocument(_EditorTexts);
        }
    }
}
