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
            Document = new FormattedStrings(new List<FormattedString>());
        }
        public abstract void Format();
        public IEnumerable<string> OutputMessages
        {
            get {
                return EditorDocument.Reader.GetOutputMessages();
            }
        }
        public void Open(ProjectFile file)
        {
            EditorDocument.Open(file);
            OnDocumentUpdate?.Invoke(this, null);
        }
        public void Save()
        {
            EditorDocument.Save();
        }
        public event EventHandler OnDocumentUpdate;
        private FormattedStrings _Document;
        public FormattedStrings Document { get => _Document; protected set { _Document = value; OnDocumentUpdate?.Invoke(this, null); } }
        public EditorDocument EditorDocument { get; protected set; }
    }
}
