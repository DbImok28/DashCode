using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public abstract class DocumentReader
    {
        public abstract IConstruction Read(EditorDocument document);
        public abstract List<string> GetOutputMessages();
        //public abstract IConstruction AddSymbolToDocument(int editPos, string newSymbols);
        //public abstract IConstruction RemoveSymbolFromDocument(int editPos, int editLength);
    }
}
