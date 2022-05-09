using DashCode.Models.Document;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DashCode.Models.CSharpReader
{
    /// <summary>
    /// Разбивает текст на токены
    /// </summary>
    public class CSharpScaner : IDocumentScaner
    {
        /*
         * [a-zA-Z_]\w* // Name 
         * [$|@]?\"[\w\s]*\" // strings
         * [1-9][0-9]* // Number
         * [1-9]\d*(?:\.\d+)? // Float
         */
        public List<Token> Scane(string rawDocument)
        {
            List<Token> tokens = new List<Token>();

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
                            tokens.Add(MakeToken(builder.ToString()));
                            builder.Clear();
                        }
                        tokens.Add(MakeToken(symbol.ToString()));
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
                        tokens.Add(MakeToken(builder.ToString()));
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
            foreach (var regex in TokenTypeRegices)
            {
                if (regex.Regex.IsMatch(str))
                    return new CSharpToken(str, regex.tokenType);
            }
            return new CSharpToken(str, CSharpTokenType.None);
        }
        public static Dictionary<string, CSharpTokenType> TokenTypeDictionary { get; set; }
        public static List<char> Separators { get; set; }
        public static List<(Regex Regex, CSharpTokenType tokenType)> TokenTypeRegices { get; set; }
        static CSharpScaner()
        {
            TokenTypeDictionary = new Dictionary<string, CSharpTokenType>()
            {
                { "public",     CSharpTokenType.AccessModifier },
                { "private",    CSharpTokenType.AccessModifier },
                { "protected",  CSharpTokenType.AccessModifier },
                { "internal",   CSharpTokenType.AccessModifier },
                { "class",      CSharpTokenType.KeyName },
                { "namespace",  CSharpTokenType.KeyName },
                { "using",      CSharpTokenType.KeyName },
                { "interface",  CSharpTokenType.KeyName },
                { "event",      CSharpTokenType.KeyName },
                { ";",          CSharpTokenType.Separator },
                { ",",          CSharpTokenType.Separator },
                { "(",          CSharpTokenType.ParamsStart },
                { ")",          CSharpTokenType.ParamsEnd },
                { "{",          CSharpTokenType.ScopeStart },
                { "}",          CSharpTokenType.ScopeEnd },
            };
            Separators = new List<char>()
            {
                ';', '(', ')', '{', '}', ','
            };
            TokenTypeRegices = new List<(Regex Regex, CSharpTokenType tokenType)>()
            {
                //(new Regex(@"[a-zA-Z_][\w.]*"), CSharpTokenType.TypeName),
                (new Regex(@"[a-zA-Z_]\w*"), CSharpTokenType.Name),
            };
        }
    }
}
