using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public class CSharpScaner : IDocumentScaner
    {
        public Dictionary<string, CSharpTokenType> TokenTypeDictionary { get; set; }
        public List<char> Separators { get; set; }
        public CSharpScaner()
        {
            TokenTypeDictionary = new Dictionary<string, CSharpTokenType>()
            {
                { "class", CSharpTokenType.KeyName },
                { "public", CSharpTokenType.Modifier },
                { "private", CSharpTokenType.Modifier },
                { "protected", CSharpTokenType.Modifier },
                { "internal", CSharpTokenType.Modifier },
                { "namespace", CSharpTokenType.KeyName },
                { "using", CSharpTokenType.KeyName },
                { "interface", CSharpTokenType.KeyName },
                { "event", CSharpTokenType.KeyName },
                { ";", CSharpTokenType.Separator },
                { "(", CSharpTokenType.ScopeStart },
                { ")", CSharpTokenType.ScopeEnd },
                { "{", CSharpTokenType.ScopeStart },
                { "}", CSharpTokenType.ScopeEnd },
            };
            Separators = new List<char>()
            {
                ';', '(', ')', '{', '}'
            };
        }
        public Stack<Token> Scane(string rawDocument)
        {
            Stack<Token> tokens = new Stack<Token>();

            var builder = new StringBuilder();
            for (int i = 0; i < rawDocument.Length; i++)
            {
                char symbol = rawDocument[i];
                if (!char.IsWhiteSpace(symbol))
                {
                    if(Separators.Contains(symbol))
                    {
                        if (builder.Length > 0)
                        {
                            tokens.Push(MakeToken(builder.ToString()));
                            builder.Clear();
                        }
                        tokens.Push(MakeToken(symbol.ToString()));
                    }
                    else
                    {
                        builder.Append(symbol);
                    }
                }
                else
                {
                    if (builder.Length > 0)
                    {
                        tokens.Push(MakeToken(builder.ToString()));
                        builder.Clear();
                    }
                }
            }
            return tokens;
        }
        public Token MakeToken(string str)
        {
            if(TokenTypeDictionary.TryGetValue(str, out CSharpTokenType type))
            {
                return new CSharpToken(str, type);
            }
            return new CSharpToken(str, CSharpTokenType.None);
        }
    }
}
