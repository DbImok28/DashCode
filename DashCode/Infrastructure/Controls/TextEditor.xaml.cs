using DashCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DashCode.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    public partial class TextEditor : UserControl
    {
        public List<string> ContentAssistSource
        {
            get { return (List<string>)GetValue(ContentAssistSourceProperty); }
            set { SetValue(ContentAssistSourceProperty, value); }
        }
        public static readonly DependencyProperty ContentAssistSourceProperty =
            DependencyProperty.Register("ContentAssistSource", typeof(List<string>), typeof(TextEditor), new UIPropertyMetadata(new List<string>()));


        public TextEditor()
        {
            InitializeComponent();

            InitRichTextBoxSource();
            editorRTB.Focus();
            DataContext = this;
            editorRTB.ContentAssistSource = ContentAssistSource;

            ReadAndUpdate();
        }

        private void InitRichTextBoxSource()
        {
            ContentAssistSource = new List<string>(){ "abstract",
                "as",
                "base",
                "bool",
                "break",
                "byte",
                "case",
                "catch",
                "char",
                "checked",
                "class",
                "const",
                "continue",
                "decimal",
                "default",
                "delegate",
                "do",
                "double",
                "else",
                "enum",
                "event",
                "explicit",
                "extern",
                "false",
                "finally",
                "fixed",
                "float",
                "for",
                "foreach",
                "goto",
                "if",
                "implicit",
                "in",
                "int",
                "interface",
                "internal",
                "is",
                "lock",
                "long",
                "namespace",
                "new",
                "null",
                "object",
                "operator",
                "out",
                "override",
                "params",
                "private",
                "protected",
                "public",
                "readonly",
                "ref",
                "return",
                "sbyte",
                "sealed",
                "short",
                "sizeof",
                "stackalloc",
                "static",
                "string",
                "struct",
                "switch",
                "this",
                "throw",
                "true",
                "try",
                "typeof",
                "uint",
                "ulong",
                "unchecked",
                "unsafe",
                "ushort",
                "using",
                "virtual",
                "void",
                "volatile",
                "while",
                "add",
                "and",
                "alias",
                "ascending",
                "args",
                "async",
                "await",
                "by",
                "descending",
                "dynamic",
                "equals",
                "from",
                "get",
                "global",
                "group",
                "init",
                "into",
                "join",
                "let",
                "managed",
                "nameof",
                "nint",
                "not",
                "notnull",
                "nuint",
                "on",
                "or",
                "orderby",
                "partial",
                "record",
                "remove",
                "select",
                "set",
                "unmanaged",
                "value",
                "var",
                "when",
                "where",
                "with",
                "yield"};
        }

        #region Color
        private bool IgnoreChange = false;
        private int rowCount = 0;
        public int EditsCount { get; set; } = 0;
        public void ReadAndUpdate(int offset = 0, int len = 0)
        {
            var range = new TextRange(editorRTB.Document.ContentStart, editorRTB.Document.ContentEnd);
            App.VMService.MainVM.FormattedDocument.EditorDocument.SetText(range.Text);
            Update(offset, len);
        }
        public void Update(int offset = 0, int len = 0)
        {
            EditsCount = 0;
            var mainVM = App.VMService.MainVM;
            mainVM.FormattedDocument.EditorDocument.Read();
            mainVM.FormattedDocument.Format();
            ConvertAndSet(mainVM.FormattedDocument);
            mainVM.OnPropertyChanged("FormattedDocument");
            try
            {
                editorRTB.CaretPosition = editorRTB.Document.ContentStart.GetPositionAtOffset(offset + len);
            }
            catch (Exception)
            {

                editorRTB.CaretPosition = editorRTB.Document.ContentStart.GetPositionAtOffset(0);
            }
        }
        public List<Block> ConvertToBlocks(FormattedEditorDocument formattedDoc)
        {
            var blocks = new List<Block>();
            Paragraph currentParagraph = new Paragraph();
            foreach (var str in formattedDoc.Document)
            {
                var splited = Regex.Split(str.Text, @"\r?\n");
                var brush = new SolidColorBrush(str.TextColor);
                var run = new Run(splited[0])
                {
                    Foreground = brush
                };
                currentParagraph.Inlines.Add(run);

                for (int i = 1; i < splited.Length; i++)
                {
                    blocks.Add(currentParagraph);
                    currentParagraph = new Paragraph();
                    var nextRun = new Run(splited[i])
                    {
                        Foreground = brush
                    };
                    currentParagraph.Inlines.Add(nextRun);
                }
            }
            blocks.Add(currentParagraph);
            return blocks;
        }
        private void ConvertAndSet(FormattedEditorDocument formattedDoc)
        {
            SetBlocks(ConvertToBlocks(formattedDoc));
        }
        private ScrollViewer FindScrollViewer(DependencyObject d)
        {
            if (d is ScrollViewer)
                return d as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var sw = FindScrollViewer(VisualTreeHelper.GetChild(d, i));
                if (sw != null) return sw;
            }
            return null;
        }
        private void SetBlocks(List<Block> blocks)
        {
            TextPointer tp1 = editorRTB.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = editorRTB.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);

            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            editorRTB.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            //Title = "Line: " + currentLineNumber.ToString() + " Column: " + column.ToString();

            rowCount = blocks.Count();
            IgnoreChange = true;
            editorRTB.Document.Blocks.Clear();
            editorRTB.Document.Blocks.AddRange(blocks);
            editorRTB.UpdateLayout();
            IgnoreChange = false;
        }
        private void editorRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IgnoreChange)
            {
                foreach (var change in e.Changes)
                {
                    EditsCount += change.AddedLength + change.RemovedLength;
                    if (EditsCount > 20)
                    {
                        ReadAndUpdate(change.Offset, change.AddedLength - change.RemovedLength);
                        EditsCount = 0;
                    }
                }
            }
        }
        private void editorRTB_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //int startRow = (int)(e.VerticalOffset / 48) + 1;
            int startRow = (int)(e.VerticalOffset / 24);
            if (startRow + 1 < rowCount - startRow)
                rowList.ItemsSource = Enumerable.Range(startRow + 1, rowCount - startRow).ToArray();
        }
        #endregion
    }
}