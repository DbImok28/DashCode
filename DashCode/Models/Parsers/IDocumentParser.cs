using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentParser
{
    public interface IDocumentParser
    {
        public void ParseDocument(string document);
        public void AddSymbolToDocument(string document, int editPos, string newSymbols);
        public void RemoveSymbolFromDocument(string document, int editPos, int editLength);
        public IConstruction ParsedDocument { get; set; }
    }
}
