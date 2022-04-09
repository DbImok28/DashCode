using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public enum NodeType
    {
        None,
        Root,
        Scope,
        Params,
        Using,
        Namespace,
        Class,
        Property,
        Interface,
        Event,
        Method,
        Var
    }
    public enum AccessModificators
    {
        None,
        Public,
        Private,
        Protected,
        Internal,
        PrivateProtected,
        ProtectedInternal
    }
    public class BaseNode : IConstruction
    {
        public bool CheckInterval(int pos, int len, int iterPos)
        {
            return false;
        }
        public FormattedStrings ApplyFormat(FormattedStrings strings)
        {
            throw new NotImplementedException();
        }
        public virtual bool Check()
        {
            return false;
        }
        public virtual string GetErrorMessage()
        {
            return ErrorMessage;
        }
        public void AddNode(BaseNode node)
        {
            SubNodes.Add(node);
        }
        public NodeType NodeType { get; set; }
        public List<BaseNode> SubNodes { get; set; }
        public string ErrorMessage { get; protected set; }
        public BaseNode(NodeType nodeType = NodeType.None)
        {
            NodeType = nodeType;
            SubNodes = new List<BaseNode>();
        }
        public override string ToString()
        {
            if (SubNodes.Count > 0)
            {
                var builder = new StringBuilder();
                builder.Append(NodeType);
                builder.Append(" (");
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1) 
                        builder.Append(", ");
                }
                builder.Append(")");
                return builder.ToString();
            }
            return NodeType.ToString();
        }
    }
    public class DeductionNode : BaseNode
    {
        static DeductionNode()
        {
            KeyNameDictionary = new Dictionary<string, NodeType>()
            {
                { "class",      NodeType.Class },
                { "namespace",  NodeType.Namespace },
                { "using",      NodeType.Using },
                { "interface",  NodeType.Interface },
                { "event",      NodeType.Event },
            };
        }
        public DeductionNode(List<Token> tokens, NodeType nodeType = NodeType.None) : base(nodeType)
        {
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            AccessModificator = AccessModificators.None;
        }
        public bool DetermineNodeType()
        {
            if (NodeType == NodeType.None)
            {
                int startIndex = DetermineAccessModificator();
                int NameCount = 0;
                int NameIndex = -1;
                for (int i = startIndex; i < Tokens.Count; i++)
                {
                    var token = Tokens[i] as CSharpToken;
                    switch (token.TokenType)
                    {
                        case CSharpTokenType.None:
                            ErrorMessage = "Unknown expression";
                            return false;
                        case CSharpTokenType.AccessModifier:
                            ErrorMessage = "Incorrect access modifier declaration";
                            return false;
                        case CSharpTokenType.KeyName:
                            if (NodeType == NodeType.None)
                                NodeType = GetNodeType(token);
                            else
                            {
                                ErrorMessage = "Keyword declared twice";
                                return false;
                            }
                            break;
                        case CSharpTokenType.Name:
                            NameCount++;
                            if(NameIndex == -1)
                            {
                                NameIndex = i;
                            }
                            break;
                        case CSharpTokenType.TypeName:
                            if (NameIndex == -1)
                            {
                                NameIndex = i;
                            }
                            NameCount++;
                            break;
                        default:
                            ErrorMessage = "Unknown error";
                            break;
                    }
                }
                if (NodeType == NodeType.None)
                {
                    if (SubNodes.Count == 2 && SubNodes[0].NodeType == NodeType.Params && SubNodes[1].NodeType == NodeType.Scope)
                    {
                        NodeType = NodeType.Method;
                    }
                    else if (NameCount == 2)
                    {
                        if (SubNodes.Count == 1 && SubNodes[0].NodeType == NodeType.Scope)
                        {
                            NodeType = NodeType.Property;
                        }
                        else if (SubNodes.Count == 0)
                        {
                            NodeType = NodeType.Var;
                        }
                    }
                }
                switch (NodeType)
                {
                    case NodeType.None:
                        break;
                    case NodeType.Using:
                    case NodeType.Namespace:
                    case NodeType.Interface:
                    case NodeType.Class:
                        if(NameCount == 1)
                        {
                            CSharpToken typeNameToken = Tokens[NameIndex] as CSharpToken;
                            if (typeNameToken.TokenType == CSharpTokenType.Name || typeNameToken.TokenType == CSharpTokenType.TypeName)
                            {
                                Name = typeNameToken.Text;
                            }
                            else
                            {
                                ErrorMessage = "Unknown error";
                                return false;
                            }
                        }
                        break;
                    case NodeType.Property:
                    case NodeType.Event:
                    case NodeType.Method:
                    case NodeType.Var:
                        if (NameCount == 2)
                        {
                            CSharpToken typeNameToken = Tokens[NameIndex] as CSharpToken;
                            CSharpToken nameToken = Tokens[NameIndex + 1] as CSharpToken;
                            if (typeNameToken.TokenType == CSharpTokenType.Name || typeNameToken.TokenType == CSharpTokenType.TypeName && nameToken.TokenType == CSharpTokenType.Name)
                            {
                                TypeName = typeNameToken.Text;
                                if (nameToken.TokenType == CSharpTokenType.Name)
                                {
                                    Name = nameToken.Text;
                                }
                                else
                                {
                                    ErrorMessage = "No name specified";
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Type not specified";
                                return false;
                            }
                        }
                        break;
                    default:
                        ErrorMessage = "Unknown error";
                        return false;
                }
            }
            return true;
        }
        private int DetermineAccessModificator()
        {
            if((Tokens[0] as CSharpToken).TokenType == CSharpTokenType.AccessModifier)
            {
                switch (Tokens[0].Text)
                {
                    case "public":
                        AccessModificator = AccessModificators.Public;
                        return 1;
                    case "private":
                        if (Tokens[1].Text == "protected")
                        {
                            AccessModificator = AccessModificators.PrivateProtected;
                            return 2;
                        }
                        AccessModificator = AccessModificators.Private;
                        return 1;
                    case "protected":
                        if (Tokens[1].Text == "internal")
                        {
                            AccessModificator = AccessModificators.ProtectedInternal;
                            return 2;
                        }
                        AccessModificator = AccessModificators.Protected;
                        return 1;
                    case "internal":
                        AccessModificator = AccessModificators.Internal;
                        return 1;
                    default:
                        break;
                }
            }
            return 0;
        }
        public NodeType GetNodeType(CSharpToken token)
        {
            if (token.TokenType == CSharpTokenType.KeyName)
            {
                if (KeyNameDictionary.TryGetValue(token.Text, out var value))
                {
                    return value;
                }
            }
            return NodeType.None;
        }
        public static Dictionary<string, NodeType> KeyNameDictionary { get; set; }
        public AccessModificators AccessModificator { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(NodeType);
            if (Tokens.Count > 0)
            {
                builder.Append(" [");
                for (int i = 0, n = Tokens.Count; i < n; i++)
                {
                    var token = Tokens[i];
                    builder.Append(token);
                    if (i < n - 1)
                        builder.Append(", ");
                }
                builder.Append("]");
            }
            if (SubNodes.Count > 0)
            {
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1)
                        builder.Append(", \n");
                }
            }
            return builder.ToString();
        }
        public override bool Check()
        {
            return DetermineNodeType();
        }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public List<Token> Tokens { get; set; }
    }
    public class RootNode : BaseNode
    {
        public RootNode() : base(NodeType.Root)
        {
            SubNodes = new List<BaseNode>();
        }
        public override bool Check()
        {
            return true;
        }
        public override string ToString()
        {
            if (SubNodes.Count > 0)
            {
                var builder = new StringBuilder();
                builder.Append(NodeType);
                builder.Append(' ');
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1)
                        builder.Append(", \n");
                }
                return builder.ToString();
            }
            return NodeType.ToString();
        }
    }
    public class ScopeNode : BaseNode 
    {
        public ScopeNode() : base(NodeType.Scope)
        {
        }
        public override bool Check()
        {
            return true;
        }
        public override string ToString()
        {
            if (SubNodes.Count > 0)
            {
                var builder = new StringBuilder();
                builder.Append(NodeType);
                builder.Append(" {");
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1)
                        builder.Append(", \n");
                }
                builder.Append("}");
                return builder.ToString();
            }
            return NodeType.ToString();
        }
    }
    public class ParamsNode : BaseNode
    {
        public ParamsNode() : base(NodeType.Params)
        {
           
        }
        public override bool Check()
        {
            return true;
        }
        public override string ToString()
        {
            if (SubNodes.Count > 0)
            {
                var builder = new StringBuilder();
                builder.Append(NodeType);
                builder.Append(" (");
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1)
                        builder.Append(", ");
                }
                builder.Append(")");
                return builder.ToString();
            }
            return NodeType.ToString();
        }
    }
   
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
 
}
