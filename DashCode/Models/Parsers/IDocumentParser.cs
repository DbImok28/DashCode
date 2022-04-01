using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentParser
{
    public interface IDocumentParser
    {
        IConstruction ParseDocument(string document);
        IConstruction AddSymbolToDocument(string document, IConstruction parsedDoc, int editPos, string newSymbols);
        IConstruction RemoveSymbolFromDocument(string document, IConstruction parsedDoc, int editPos, int editLength);
    }
}
