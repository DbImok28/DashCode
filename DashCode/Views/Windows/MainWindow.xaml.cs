using DashCode.Models;
using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashCode.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // TODO: Change to INotify
            InitializeComponent();
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                this.mainWindowViewModel = mainWindowViewModel;
                ConvertAndSet(mainWindowViewModel.FormattedDocument);
            }
            else
            {
                throw new InvalidCastException("Window DataContext is not MainWindowViewModel");
            }
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
                        Update(change.Offset, change.AddedLength - change.RemovedLength);
                        EditsCount = 0;
                    }
                }
            }
        }
        public void Update(int offset, int len)
        {
            var range = new TextRange(editorRTB.Document.ContentStart, editorRTB.Document.ContentEnd);
            mainWindowViewModel.FormattedDocument.EditorDocument.SetText(range.Text);
            mainWindowViewModel.FormattedDocument.EditorDocument.Read();
            mainWindowViewModel.FormattedDocument.Format();
            ConvertAndSet(mainWindowViewModel.FormattedDocument);
            mainWindowViewModel.OnPropertyChanged("FormattedDocument");
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

            Title = "Line: " + currentLineNumber.ToString() + " Column: " + column.ToString();
            
            rowCount = blocks.Count();
            IgnoreChange = true;
            editorRTB.Document.Blocks.Clear();
            editorRTB.Document.Blocks.AddRange(blocks);
            editorRTB.UpdateLayout();
            IgnoreChange = false;
        }
        private void Button_UpdateClick(object sender, RoutedEventArgs e)
        {
            Update(0, 0);
            EditsCount = 0;
        }
        // TODO: Store only document
        private MainWindowViewModel mainWindowViewModel;
        private bool IgnoreChange = false;
        private int EditsCount = 0;
        private int rowCount = 0;

        private void editorRTB_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //int startRow = (int)(e.VerticalOffset / 48) + 1;
            int startRow = (int)(e.VerticalOffset / 24);
            rowList.ItemsSource = Enumerable.Range(startRow + 1, rowCount - startRow).ToArray();
        }
    }
}
