using DashCode.Models.DocumentParser;
using System;

namespace DashCode.Models.Parsers.CSharp
{
    public class CSharpDocumentParser : IDocumentParser
    {
        public CSharpDocumentParser()
        {

        }

        public IConstruction ParsedDocument { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddSymbolToDocument(string document, int editPos, string newSymbols)
        {
            throw new NotImplementedException();
        }

        public void ParseDocument(string document)
        {
            throw new NotImplementedException();
        }

        public void RemoveSymbolFromDocument(string document, int editPos, int editLength)
        {
            throw new NotImplementedException();
        }
    }
}
