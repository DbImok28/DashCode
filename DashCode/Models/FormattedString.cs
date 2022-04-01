using System;
using System.Windows.Media;

namespace DashCode.Models
{
    public class FormattedString
    {
        public FormattedString(string text, Color textColor)
        {
            Text = text;
            TextColor = textColor;
        }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public int TextLength => Text.Length;
    }
}
