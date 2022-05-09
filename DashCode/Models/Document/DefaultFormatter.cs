using System.Collections.Generic;

namespace DashCode.Models.Document
{
    public class DefaultFormatter : DocumentFormatter
    {
        public override FormattedStrings Format(EditorDocument document)
        {
            return new FormattedStrings(new List<FormattedString>() { new FormattedString(document.RawDocument, DocumentFormatter.NoFormattedTextColor) });
        }
    }
}
