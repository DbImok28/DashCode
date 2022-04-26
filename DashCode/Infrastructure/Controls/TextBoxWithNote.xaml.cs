using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashCode.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxWithNote.xaml
    /// </summary>
    public partial class TextBoxWithNote : UserControl
    {
        public TextBoxWithNote()
        {
            InitializeComponent();
        }
        public string NoteText
        {
            get { return (string)GetValue(NoteTextProperty); }
            set { SetValue(NoteTextProperty, value); }
        }
        public static readonly DependencyProperty NoteTextProperty =
            DependencyProperty.Register("NoteText", typeof(string), typeof(TextBoxWithNote), new PropertyMetadata("note"));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithNote), new PropertyMetadata(""));
        private void InputTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = InputTextBlock.Text;
            if(Text == "")
            {
                NoteTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                NoteTextBlock.Visibility = Visibility.Hidden;
            }
        }
    }
}
