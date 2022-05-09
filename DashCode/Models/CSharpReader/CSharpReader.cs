using DashCode.Models.Document;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.CSharpReader
{
    public class CSharpReader : DocumentReader
    {
        public CSharpReader()
        {
            Parser = new CSharpParser();
            Scaner = new CSharpScaner();
        }
        public DocumentParser Parser { get; private set; }
        public IDocumentScaner Scaner { get; private set; }
        public EditorDocument Document { get; private set; } = null;
        public List<Token> ScaneOnly(EditorDocument document)
        {
            Document = document;
            return Scaner.Scane(Document.RawDocument);
        }
        public override IConstruction Read(EditorDocument document)
        {
            Document = document;
            try
            {
                var tokens = Scaner.Scane(Document.RawDocument);
                return Parser.Parse(tokens);
            }
            catch (System.Exception)
            {
                Parser.OutputLog.Add("Scane error");
                return null;
            }
        }

        public override List<string> GetOutputMessages()
        {
            return Parser.OutputLog;
        }
    }
}
