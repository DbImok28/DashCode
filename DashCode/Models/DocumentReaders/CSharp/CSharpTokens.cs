namespace DashCode.Models.DocumentReaders.CSharp
{
    public enum CSharpTokenType
    {
        None,
        Modifier,
        KeyName,
        Separator,
        ScopeStart,
        ScopeEnd,
        Name,
        ClassName,
        MethodName,
        PropertyName,
        VarName,
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
        public CSharpTokenType TokenType { get; }
        public override string ToString()
        {
            return $"{TokenType}: {Text}";
        }
    }
}
