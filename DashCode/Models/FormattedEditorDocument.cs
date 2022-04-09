using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DashCode.Models
{
    public abstract class FormattedEditorDocument
    {
        public FormattedEditorDocument(EditorDocument editorDocument)
        {
            EditorDocument = editorDocument ?? throw new ArgumentNullException(nameof(editorDocument));
            //Test
            Document = Document = new FormattedStrings(new List<FormattedString>
            {
                new FormattedString("int", Color.FromRgb(0,255,0))
            });
            //Color keyNamesColor = Color.FromRgb(0, 0, 255);
            //Color variableColor = Color.FromRgb(191, 141, 80);
            //Color defaultColor = Color.FromRgb(0, 0, 0);
            //Document = new FormattedStrings(new List<FormattedString>{
            //            new FormattedString("int", keyNamesColor),
            //            new FormattedString(" ", defaultColor),
            //            new FormattedString("Count", variableColor),
            //            new FormattedString(";", defaultColor),
            //            new FormattedString("\n", defaultColor),
            //            new FormattedString("double", keyNamesColor),
            //            new FormattedString(" ", defaultColor),
            //            new FormattedString("Value", variableColor),
            //            new FormattedString(";", defaultColor)
            //        }
            //) ;
        }
        public abstract void Format();
        public event EventHandler OnDocumentUpdate;
        public FormattedStrings Document { get; protected set; }
        public EditorDocument EditorDocument { get; protected set; }
    }
}
