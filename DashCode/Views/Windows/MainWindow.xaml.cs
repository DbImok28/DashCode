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
            InitializeComponent();

            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                this.mainWindowViewModel = mainWindowViewModel;
                this.mainWindowViewModel.OnEditorDocumentChenged += EditorDocumentChanged;
                editorRTB.Document = this.mainWindowViewModel.EditorDocument;
            }
            else
            {
                throw new InvalidCastException("Window DataContext is not MainWindowViewModel");
            }
        }
        
        public void EditorDocumentChanged(object o, EventArgs args)
        {
            editorRTB.UpdateLayout();
        }

        private void editorRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            outDebug.Text = mainWindowViewModel.EditorDocument.ContentStart.GetTextInRun(LogicalDirection.Backward);
            outDebug.UpdateLayout();
            //var doc = ((RichTextBox)sender).Document;
            //foreach (var change in e.Changes)
            //{
            //    var offs = change.Offset;
            //    var ptr = doc.ContentStart.GetPositionAtOffset(offs);
            //    var paragraph = ptr.Paragraph;
            //    var parIndex = ((System.Collections.IList)doc.Blocks).IndexOf(paragraph);
            //    ChangeTexts.Add(
            //        $"{changeNo}: added {change.AddedLength}, removed {change.RemovedLength}" +
            //        $" at position {change.Offset}, paragraph #{parIndex}");
            //}
            foreach (var change in e.Changes)
            {
                if (change.AddedLength != 0)
                {
                    mainWindowViewModel.AddText(change.Offset, CalculateChangedText(change));
                }
                else
                {
                    // TODO: MultiEdit
                    var ptr1 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset);
                    //var ptr2 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset + change.RemovedLength);
                    var ptr2 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset + change.RemovedLength);
                    var pos = new TextRange(ptr2, ptr1);
                    char[] buff = new char[128]; ;
                    var str = ptr1.GetTextInRun(LogicalDirection.Backward, buff, change.Offset, change.RemovedLength);
                    //var range = new TextRange(ptr.get)
                    //var paragraph = ptr.Paragraph;
                    //var parIndex = ((System.Collections.IList)doc.Blocks).IndexOf(paragraph);
                    //outDebug.Text = ((Run)ptr1.Parent).Text;
                    outDebug.Text = "";
                    foreach (var item in buff)
                    {
                        outDebug.Text += item;
                    }
                    outDebug.UpdateLayout();
                    mainWindowViewModel.LastTextChange = change;
                    mainWindowViewModel.RemoveText(change.Offset, change.RemovedLength);
                }

            }
        }
        private string CalculateChangedText(TextChange change)
        {
            string result = null;
            int currentPos = 0;
            foreach (var item in mainWindowViewModel.EditorTexts)
            {
                currentPos += item.TextLength;
                if (currentPos > change.Offset)
                {
                    result = item.Text[currentPos - change.Offset - 1].ToString();             
                }
            }
            if(result == null)
            {
                throw new IndexOutOfRangeException("Invalid edit");
            }
            return result;
        }
        private MainWindowViewModel mainWindowViewModel;

    }
}
