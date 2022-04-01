using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public interface IDocumentScaner
    {
        public Stack<Token> Scane(string rawDocument);
    }
}
