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
            //string result = null;
            //int currentPos = 0;
            //foreach (var item in mainWindowViewModel.EditorTexts)
            //{
            //    currentPos += item.TextLength;
            //    if (currentPos > change.Offset)
            //    {
            //        result = item.Text[currentPos - change.Offset - 1].ToString();             
            //    }
            //}
            //if(result == null)
            //{
            //    throw new IndexOutOfRangeException("Invalid edit");
            //}
            //return result;

            // TODO: MultiEdit
            var ptr1 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset);
            //var ptr2 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset + change.RemovedLength);
            //var ptr2 = mainWindowViewModel.EditorDocument.ContentStart.GetPositionAtOffset(change.Offset + 1);
            //var pos = new TextRange(ptr2, ptr1);
            char[] buff = new char[change.Offset + change.RemovedLength];
            var count = ptr1.GetTextInRun(LogicalDirection.Backward, buff, change.Offset, change.RemovedLength);
            var str = new string(buff);
            return str;
        }
        private MainWindowViewModel mainWindowViewModel;

    }
}
