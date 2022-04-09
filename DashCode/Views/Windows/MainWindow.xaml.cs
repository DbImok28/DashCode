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

namespace DashCode
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
                //this.mainWindowViewModel.FormattedDocument.OnDocumentUpdate += EditorDocumentChanged;
                //flowDocument = ConvertToFlowDocument(mainWindowViewModel.FormattedDocument);
                ConvertAndSet(mainWindowViewModel.FormattedDocument);
                //editorRTB.Document = flowDocument;
            }
            else
            {
                throw new InvalidCastException("Window DataContext is not MainWindowViewModel");
            }
        }
        
        //public void EditorDocumentChanged(object o, EventArgs args)
        //{
        //    //var range = new TextRange(editorRTB.Document.ContentStart, editorRTB.Document.ContentEnd);
        //    //range.Changed

        //    //flowDocument = ConvertToFlowDocument(mainWindowViewModel.FormattedDocument);
        //    ConvertToFlowDocument(mainWindowViewModel.FormattedDocument);
        //    editorRTB.UpdateLayout();
        //}

        private void editorRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IgnoreChange)
            {
                foreach (var change in e.Changes)
                {
                    EditsCount += change.AddedLength + change.RemovedLength;
                }
                if (EditsCount > 10)
                {
                    EditsCount = 0;
                    var range = new TextRange(editorRTB.Document.ContentStart, editorRTB.Document.ContentEnd);
                    mainWindowViewModel.FormattedDocument.EditorDocument.SetText(range.Text);
                    mainWindowViewModel.FormattedDocument.EditorDocument.Read();
                    mainWindowViewModel.FormattedDocument.Format();
                    ConvertAndSet(mainWindowViewModel.FormattedDocument);
                }
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
        private void SetBlocks(List<Block> blocks)
        {
            IgnoreChange = true;
            editorRTB.Document.Blocks.Clear();
            editorRTB.Document.Blocks.AddRange(blocks);
            editorRTB.UpdateLayout();
            IgnoreChange = false;
        }
        // TODO: Store only document
        private MainWindowViewModel mainWindowViewModel;
        private bool IgnoreChange = false;
        private int EditsCount = 0;
        private int LastUpdateSeconds = 0;
    }
}
