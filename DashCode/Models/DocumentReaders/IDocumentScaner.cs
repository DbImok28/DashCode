using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public interface IDocumentScaner
    {
        public List<Token> Scane(string rawDocument);
    }
}
