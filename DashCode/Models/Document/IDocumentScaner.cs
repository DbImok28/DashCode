using System.Collections.Generic;

namespace DashCode.Models.Document
{
    public interface IDocumentScaner
    {
        public List<Token> Scane(string rawDocument);
    }
}
