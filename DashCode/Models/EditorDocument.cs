using DashCode.Models.DocumentReaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DashCode.Models
{
    public class EditorDocument
    {
        public EditorDocument(string rawDocument, DocumentReader reader)
        {
            if (string.IsNullOrWhiteSpace(rawDocument))
            {
                throw new ArgumentException($"'{nameof(rawDocument)}' cannot be null or whitespace.", nameof(rawDocument));
            }

            RawDocument = rawDocument;
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public IConstruction ReadedDocument { get; private set; }
        public string RawDocument { get; private set; }
        public DocumentReader Reader { get; set; }
        //public event EventHandler OnDocumentUpdate;
        //public void AddText(int pos, string str)
        //{
        //    RawDocument = RawDocument.Insert(pos, str);
        //    OnDocumentUpdate?.Invoke(this, null);
        //}
        //public void RemoveText(int pos, int length)
        //{
        //    RawDocument = RawDocument.Remove(pos, length);
        //    OnDocumentUpdate?.Invoke(this, null);
        //}
        public void SetText(string document)
        {
            RawDocument = document;
            //OnDocumentUpdate?.Invoke(this, null);
        }
        public void Read()
        {
            var doc = Reader.Read(this);
            if (doc != null)
                ReadedDocument = doc;
        }
    }
}
