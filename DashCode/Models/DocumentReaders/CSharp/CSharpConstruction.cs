using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public enum CSharpConstructionType
    {
        None,
        Root,
        Using,
        Namespace,
        Class,
        Property,
        Method
    }
    public class CSharpConstruction : IConstruction
    {
        public bool CheckInterval(int pos, int len, int iterPos)
        {
            return false;
        }
        public FormattedStrings ApplyFormat(FormattedStrings strings)
        {
            throw new NotImplementedException();
        }
        public CSharpConstructionType ConstructionType { get; protected set; } = CSharpConstructionType.None;
        public List<Token> lexemes;
    }
    //public class DocumentConstruction : IConstruction
    //{
    //    public IEnumerable<ILexeme> Lexemes { get; private set; }

    //    public FormattedStrings GetFormattedStrings()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool ParseBody(string text)
    //    {
    //        //// (\w|\.|_)+ 
    //        //// (public|private|protected|internal|private\sprotected|protected\sinternal)(\s)+
    //        //// CtorDeduction (public|private|protected|internal|private\sprotected|protected\sinternal)\s+(\w)+\s*    Brek
    //        //// TypeOrMethodDeduction (public|private|protected|internal|private\sprotected|protected\sinternal)\s+(\w|\.|_)+\s+(\w|\.|_)+\s*    Brek
    //        //var usingRegex = new Regex(@"using(\s)(\w|\.|_)+;");
    //        //var namespaceRegex = new Regex(@"namespace\s+(\w|\.|_)+\s*\{(.|\s)*\}"); // {}
    //        //var classRegex = new Regex(@"((public|private|protected|internal|private\sprotected|protected\sinternal)+\s+class\s+(\w|\.|_)+\s*\{(.|\s)*\}"); //abstract ...
    //        //var propertyRegex = new Regex(@"(public|private|protected|internal|private\sprotected|protected\sinternal)\s+(\w|\.|_)+\s+(\w|\.|_)+\s*(?:;|\{(.|\s)*\})"); // hard mod chesc
    //        //var getSetAddRemove = new Regex(@"(public|private|protected|internal|private\sprotected|protected\sinternal)?\s*(get|set|add|remove)\s*(;|=>\s*(?:\{(.|\s)*\})|(.|\s)*?;)");
    //        //var methodInputRegex = new Regex(@"\s*(\w+)\s+(\w+)\s*(\w+\s*)?(,|=\s(\w)+|)");
    //        //var varRegex = new Regex(@"");
    //        //var callRegex = new Regex(@"");
    //        //var ctorRegex = new Regex(@"");
    //        ///*
    //        // * (public|private|protected|internal|private\sprotected|protected\sinternal)\s+(\w|\.|_)+\s+(\w|\.|_)+\s*(?:;|\{\s*(public|private|protected|internal|private\sprotected|protected\sinternal)?\s+(get|set)\s*(?:;|=>\s*(.|\s)+;))
    //        // */
    //        //foreach (var item in collection)
    //        //{
    //        //}
    //        return false;
    //    }
    //}
    //public class NamespaceConstruction : IConstruction
    //{
    //    public IEnumerable<ILexeme> Lexemes { get; private set; }

    //    public FormattedStrings GetFormattedStrings()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool ParseBody(string text)
    //    {
    //        return false;
    //    }
    //}
}
