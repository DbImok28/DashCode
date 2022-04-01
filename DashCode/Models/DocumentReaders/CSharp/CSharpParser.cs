using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public class CSharpParser : IDocumentParser
    {
        public IConstruction Parse(Stack<Token> tokens)
        {
            throw new NotImplementedException();
        }
        
        public IConstruction ParseRoot(string source)
        {
            var construction = new CSharpConstruction();

            return construction;
        }
        public IConstruction TryParseNamespace(string source)
        {
            var construction = new CSharpConstruction();

            return construction;
        }
        public IConstruction TryParseClass(string source)
        {
            var construction = new CSharpConstruction();

            return construction;
        }
        public IConstruction TryParseProperty(string source)
        {
            var construction = new CSharpConstruction();

            return construction;
        }
        public IConstruction TryParseMethod(string source)
        {
            var construction = new CSharpConstruction();

            return construction;
        }
    }
}
