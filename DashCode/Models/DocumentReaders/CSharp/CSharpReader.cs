using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public class CSharpReader : IDocumentReader
    {
        public CSharpReader()
        {
            Parser = new CSharpParser();
            Scaner = new CSharpScaner();
        }
        public IDocumentParser Parser { get; private set; }
        public IDocumentScaner Scaner { get; private set; }
        public EditorDocument Document { get; private set; } = null;
        public Stack<Token> ScaneOnly(EditorDocument document)
        {
            Document = document;
            return Scaner.Scane(Document.RawDocument);
        }
        public IConstruction Read(EditorDocument document)
        {
            Document = document;
            var tokens = Scaner.Scane(Document.RawDocument);
            return Parser.Parse(tokens);
        }
    }
}
