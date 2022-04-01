using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DashCode.Models
{
    public class FormattedEditorDocument
    {
        public FormattedEditorDocument(EditorDocument editorDocument)
        {
            EditorDocument = editorDocument ?? throw new ArgumentNullException(nameof(editorDocument));
            //Test
            Color keyNamesColor = Color.FromRgb(0, 0, 255);
            Color variableColor = Color.FromRgb(191, 141, 80);
            Color defaultColor = Color.FromRgb(0, 0, 0);
            editorDocument.OnDocumentUpdate += OnUpdate;
            Document = new FormattedStrings(new List<FormattedString>{
                        new FormattedString("int", keyNamesColor),
                        new FormattedString(" ", defaultColor),
                        new FormattedString("Count", variableColor),
                        new FormattedString(";", defaultColor),
                        new FormattedString("\n", defaultColor),
                        new FormattedString("double", keyNamesColor),
                        new FormattedString(" ", defaultColor),
                        new FormattedString("Value", variableColor),
                        new FormattedString(";", defaultColor)
                    }
            );
        }
        private void OnUpdate(object o, EventArgs args)
        {
            Update();
        }
        public void Update()
        {
            Document = EditorDocument.ParsedDocument.GetFormattedStrings();
        }
        public event EventHandler OnDocumentUpdate;
        public FormattedStrings Document { get; private set; }
        public EditorDocument EditorDocument { get; private set; }
    }
}
