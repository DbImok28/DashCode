using DashCode.Models;
using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                this.mainWindowViewModel.Document.OnDocumentUpdate += EditorDocumentChanged;
                editorRTB.Document = flowDocument;
            }
            else
            {
                throw new InvalidCastException("Window DataContext is not MainWindowViewModel");
            }
        }
        
        public void EditorDocumentChanged(object o, EventArgs args)
        {
            flowDocument = ConvertToFlowDocument(mainWindowViewModel.FormattedDocument);
            editorRTB.UpdateLayout();
        }

        private void editorRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var change in e.Changes)
            {
                mainWindowViewModel.LastTextChange = change;
                if (change.AddedLength != 0)
                {
                    var str = CalculateChangedText(change);
                    outDebug.Text = str;
                    mainWindowViewModel.AddText(change.Offset, str);
                }
                else
                {
                    var str = CalculateChangedText(change);
                    outDebug.Text = str;
                    mainWindowViewModel.RemoveText(change.Offset, change.RemovedLength);
                }
                outDebug.UpdateLayout();
            }
        }
        private string CalculateChangedText(TextChange change)
        {
            // TODO: MultiEdit
            var ptr1 = flowDocument.ContentStart.GetPositionAtOffset(change.Offset);
            char[] buff = new char[change.Offset + change.RemovedLength];
            var count = ptr1.GetTextInRun(LogicalDirection.Backward, buff, change.Offset, change.RemovedLength);
            var str = new string(buff);
            return str;
        }
        public FlowDocument ConvertToFlowDocument(FormattedEditorDocument formattedDoc)
        {
            FlowDocument document = new FlowDocument();
            Paragraph currentParagraph = new Paragraph();
            foreach (var str in formattedDoc.Document)
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
        // TODO: Store only document
        private MainWindowViewModel mainWindowViewModel;
        private FlowDocument flowDocument;
    }
}
