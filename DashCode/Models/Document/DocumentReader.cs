using System.Collections.Generic;

namespace DashCode.Models.Document
{
    public abstract class DocumentReader
    {
        public abstract IConstruction Read(EditorDocument document);
        public abstract List<string> GetOutputMessages();
    }
}
