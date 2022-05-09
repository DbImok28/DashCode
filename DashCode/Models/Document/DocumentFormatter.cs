namespace DashCode.Models.Document
{
    public abstract class DocumentFormatter
    {
        public abstract FormattedStrings Format(EditorDocument document);
    }
}
