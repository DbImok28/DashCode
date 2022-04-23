using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public abstract class DocumentParser
    {
        public abstract IConstruction Parse(List<Token> tokens);
        public List<string> OutputLog { get; protected set; }
        //IConstruction AddSymbolToDocument(string document, IConstruction parsedDoc, int editPos, string newSymbols);
        //IConstruction RemoveSymbolFromDocument(string document, IConstruction parsedDoc, int editPos, int editLength);
    }
}
