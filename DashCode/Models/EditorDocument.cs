using DashCode.Models.DocumentParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DashCode.Models
{
    public class EditorDocument
    {
        public EditorDocument(string rawDocument, IDocumentParser parser)
        {
            if (string.IsNullOrWhiteSpace(rawDocument))
            {
                throw new ArgumentException($"'{nameof(rawDocument)}' cannot be null or whitespace.", nameof(rawDocument));
            }

            RawDocument = rawDocument;
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }
            
        public string RawDocument { get; private set; }
        public IConstruction ParsedDocument { get; set; }
        public IDocumentParser Parser { get; private set; }
        public event EventHandler OnDocumentUpdate;
        public void AddText(int pos, string str)
        {
            RawDocument = RawDocument.Insert(pos, str);
            OnDocumentUpdate?.Invoke(this, null);
        }
        public void RemoveText(int pos, int length)
        {
            RawDocument = RawDocument.Remove(pos, length);
            OnDocumentUpdate?.Invoke(this, null);
        }
        public void SetText(string document)
        {
            RawDocument = document;
            OnDocumentUpdate?.Invoke(this, null);
        }
        public void Parse()
        {
            Parser.ParseDocument(RawDocument);
        }
    }
}
