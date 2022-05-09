using System;

namespace DashCode.Models.Document
{
    public class EditorDocument
    {
        public EditorDocument(ProjectFile file, DocumentReader reader, DocumentFormatter formatter)
        {
            Open(file);
            SetReaderAndFormatter(reader, formatter);
        }
        public ProjectFile File { get; private set; }
        public string RawDocument => File.Content;
        public DocumentReader Reader { get; set; }
        public IConstruction ReadedDocument { get; private set; }
        public DocumentFormatter Formatter { get; set; }

        public event EventHandler OnFormattedDocumentUpdate;
        private FormattedStrings _FormattedDocument;
        public FormattedStrings FormattedDocument { get => _FormattedDocument; protected set { _FormattedDocument = value; OnFormattedDocumentUpdate?.Invoke(this, null); } }
        public void Open(ProjectFile file)
        {
            File = file;
            File.ReadContent();
        }
        public void SetText(string document)
        {
            File.Content = document;
        }
        public void Save()
        {
            File.Save();
        }
        public void Read()
        {
            var doc = Reader.Read(this);
            if (doc != null)
                ReadedDocument = doc;
        }
        public void Format()
        {
            var doc = Formatter.Format(this);
            if (doc != null)
                FormattedDocument = doc;
        }
        public void ReadAndFormat()
        {
            Read();
            Format();
        }
        public void SetReaderAndFormatter(DocumentReader reader, DocumentFormatter formatter)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }
    }
}
