using DashCode.Models.Document;

namespace DashCode.Models.CSharpReader
{
    public enum CSharpTokenType
    {
        None,
        AccessModifier,
        KeyName,
        Separator,
        ScopeStart,
        ScopeEnd,
        ParamsStart,
        ParamsEnd,
        Name,
        TypeName,
    }
    public class CSharpToken : Token
    {
        public CSharpToken(string text, CSharpTokenType tokenType = CSharpTokenType.None) : base(text)
        {
            TokenType = tokenType;
        }
        public override bool Check()
        {
            return true;
        }
        public CSharpTokenType TokenType { get; set; }

        public override string ToString()
        {
            return $"[{TokenType}: {Text}]";
        }
    }
}
