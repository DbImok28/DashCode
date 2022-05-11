using System;
using System.Collections.Generic;
using DashCode.Models.CSharpProcessing;

namespace DashCode.Models.Document
{
    public class EditorDocument
    {
        public static Dictionary<string, string> DocumentProcessing;
        public static Dictionary<string, DocumentReader> DocumentReaders;
        public static Dictionary<string, DocumentFormatter> DocumentFormatters;

        static EditorDocument()
        {
            DocumentProcessing = new Dictionary<string, string>()
            {
                { ".cs", "CSharp" }
            };
            DocumentReaders = new Dictionary<string, DocumentReader>()
            {
                { "CSharp", new CSharpReader() }
            };
            DocumentFormatters = new Dictionary<string, DocumentFormatter>()
            {
                { "CSharp", new CSharpFormatter() }
            };
        }

        public EditorDocument(ProjectFile file)
        {
            if (file == null)
            {
                Open(new ProjectFile());
            }
            else
            {
                Open(file);
            }
        }
        public ProjectFile File { get; private set; }
        public string RawDocument => File.Content;
        public DocumentReader Reader { get; set; } = null;
        public IConstruction ReadedDocument { get; private set; }
        public DocumentFormatter Formatter { get; set; } = new DefaultFormatter();
        public event EventHandler OnFormattedDocumentUpdate;
        private FormattedStrings _FormattedDocument;
        public FormattedStrings FormattedDocument { get => _FormattedDocument; protected set { _FormattedDocument = value; OnFormattedDocumentUpdate?.Invoke(this, null); } }
        public List<string> OutputMessages { get; private set; }

        public void Open(ProjectFile file)
        {
            File = file;
            File.ReadContent();
            if (file.Extension != null)
            {
                SetReaderAndFormatter(file.Extension);
            }
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
            var doc = Reader?.Read(this);
            if (doc != null)
                ReadedDocument = doc;
            OutputMessages = Reader?.GetOutputMessages();
        }
        public void Format()
        {
            var doc = Formatter?.Format(this);
            if (doc != null)
                FormattedDocument = doc;
        }
        public void ReadAndFormat()
        {
            Read();
            Format();
        }
        public void SetProcessing(string proc)
        {
            if (DocumentReaders.TryGetValue(proc, out var reader) && DocumentFormatters.TryGetValue(proc, out var formatter))
            {
                SetReaderAndFormatter(reader, formatter);
            }
            else
            {
                SetReaderAndFormatter(null, null);
            }
        }
        public void SetReaderAndFormatter(string extension)
        {
            if (DocumentProcessing.TryGetValue(extension, out var proc))
            {
                SetProcessing(proc);
            }
        }
        public void SetReaderAndFormatter(DocumentReader reader, DocumentFormatter formatter)
        {
            Reader = reader;
            if (formatter != null)
            {
                Formatter = formatter;
            }
            else
            {
                Formatter = new DefaultFormatter();
            }
        }
    }
}
