using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public interface IDocumentParser
    {
        IConstruction Parse(Stack<Token> tokens);
        //IConstruction AddSymbolToDocument(string document, IConstruction parsedDoc, int editPos, string newSymbols);
        //IConstruction RemoveSymbolFromDocument(string document, IConstruction parsedDoc, int editPos, int editLength);
    }
}
