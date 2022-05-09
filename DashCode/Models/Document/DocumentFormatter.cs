using System.Windows.Media;

namespace DashCode.Models.Document
{
    public abstract class DocumentFormatter
    {
        public abstract FormattedStrings Format(EditorDocument document);
        public static Color NoFormattedTextColor = Color.FromRgb(250, 250, 250);
        public static Color ErrorTextColor = Color.FromRgb(255, 0, 0);
    }
}
