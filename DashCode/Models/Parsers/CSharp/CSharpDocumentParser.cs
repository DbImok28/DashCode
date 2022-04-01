using DashCode.Models.DocumentParser;
using System;

namespace DashCode.Models.Parsers.CSharp
{
    public class CSharpDocumentParser : IDocumentParser
    {
        public IConstruction AddSymbolToDocument(string document, IConstruction parsedDoc, int editPos, string newSymbols)
        {
            throw new NotImplementedException();
        }

        public IConstruction ParseDocument(string document)
        {
            throw new NotImplementedException();
        }

        public IConstruction RemoveSymbolFromDocument(string document, IConstruction parsedDoc, int editPos, int editLength)
        {
            throw new NotImplementedException();
        }
    }
}
