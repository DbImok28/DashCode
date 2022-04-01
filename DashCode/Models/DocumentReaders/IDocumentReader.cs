using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public interface IDocumentReader
    {
        public abstract IConstruction Read(EditorDocument document);
        //public abstract IConstruction AddSymbolToDocument(int editPos, string newSymbols);
        //public abstract IConstruction RemoveSymbolFromDocument(int editPos, int editLength);
    }
}
