using DashCode.Models.Document;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.CSharpProcessing
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
    public enum ScopeType
    {
        None,
        Root,
        Namespace,
        Class,
        Interface,
        Property,
        Event,
        Method,
        Params
    }
    public class CSharpNode : IConstruction
    {
        public NodeType NodeType { get; set; }
        public List<CSharpNode> SubNodes { get; set; }
        public string ErrorMessage { get; protected set; }
        public static Dictionary<string, NodeType> KeyNameDictionary { get; set; }
        public AccessModificators AccessModificator { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public List<Token> Tokens { get; set; }
        public bool CheckInterval(int pos, int len, int iterPos)
        {
            return false;
        }
        public void AddNode(CSharpNode node)
        {
            SubNodes.Add(node);
        }
        static CSharpNode()
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
        public CSharpNode(List<Token> tokens, NodeType nodeType = NodeType.None)
        {
            Tokens = tokens;
            NodeType = nodeType;
            AccessModificator = AccessModificators.None;
            SubNodes = new List<CSharpNode>();
        }
        public CSharpNode(NodeType nodeType = NodeType.None) : this(new List<Token>(), nodeType) { }
        public List<string> DetermineAll(ScopeType scopeType)
        {
            var messages = new List<string>();
            var currentScope = DetermineNodeType(scopeType);
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
                messages.Add(ErrorMessage);
            if (currentScope == ScopeType.Method && SubNodes.Count == 2)
            {
                messages.AddRange(SubNodes[0].DetermineAll(ScopeType.Params));
                messages.AddRange(SubNodes[1].DetermineAll(ScopeType.Method));
            }
            else
            {
                foreach (CSharpNode node in SubNodes)
                {
                    messages.AddRange(node.DetermineAll(currentScope));
                }
            }
            return messages;
        }
        public ScopeType DetermineNodeType(ScopeType scopeType)
        {
            if (scopeType == ScopeType.None || scopeType == ScopeType.Property || scopeType == ScopeType.Method || scopeType == ScopeType.Event)
            {
                Invalidate();
                return ScopeType.None;
            }
            if (NodeType == NodeType.None)
            {
                // Before determine
                int startIndex = DetermineAccessModificator();
                int NameCount = 0;
                int NameIndex = -1;
                for (int i = startIndex; i < Tokens.Count; i++)
                {
                    var token = Tokens[i] as CSharpToken;
                    switch (token.TokenType)
                    {
                        case CSharpTokenType.None:
                            Invalidate($"Unknown expression: {token}");
                            return ScopeType.None;
                        case CSharpTokenType.AccessModifier:
                            Invalidate($"Incorrect access modifier declaration: {token}");
                            return ScopeType.None;
                        case CSharpTokenType.KeyName:
                            if (NodeType == NodeType.None)
                                NodeType = GetNodeType(token);
                            else
                            {
                                Invalidate($"Keyword declared twice: {token}");
                                return ScopeType.None;
                            }
                            break;
                        case CSharpTokenType.Name:
                        case CSharpTokenType.TypeName:
                            if (NameIndex != -1 && NameIndex == i - 1)
                            {
                                NameCount++;
                            }
                            if (NameIndex == -1)
                            {
                                NameIndex = i;
                                NameCount++;
                            }
                            break;
                        default:
                            Invalidate($"Unknown token: {token}");
                            return ScopeType.None;
                    }
                }
                // Method, Property, Var
                if (NodeType == NodeType.None)
                {
                    if (SubNodes.Count == 2 && SubNodes[0].NodeType == NodeType.Params && SubNodes[1].NodeType == NodeType.Scope)
                    {
                        NodeType = NodeType.Method;
                    }
                    else if (NameCount == 2 && Tokens.Count - startIndex == 2)
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
                // Check scope
                switch (scopeType)
                {
                    case ScopeType.Root:
                        if (NodeType != NodeType.None)
                        {
                            if (!CheckRange(new List<NodeType> {
                                NodeType.Using,
                                NodeType.Namespace,
                            })) return ScopeType.None;
                        }
                        break;
                    case ScopeType.Namespace:
                        if (NodeType != NodeType.None)
                        {
                            if (!CheckRange(new List<NodeType> {
                                NodeType.Class,
                                NodeType.Interface,
                            })) return ScopeType.None;
                        }
                        break;
                    case ScopeType.Interface:
                    case ScopeType.Class:
                        if (NodeType != NodeType.None)
                        {
                            if (!CheckRange(new List<NodeType> {
                                NodeType.Class,
                                NodeType.Property,
                                NodeType.Interface,
                                NodeType.Method,
                                NodeType.Var,
                                NodeType.Event
                            })) return ScopeType.None;
                        }
                        break;
                    case ScopeType.Params:
                        if (NodeType == NodeType.None && NameCount == 2 && Tokens.Count - startIndex == 2 && SubNodes.Count == 0)
                        {
                            NodeType = NodeType.Var;
                        }
                        break;
                    case ScopeType.Event:
                        Invalidate("Event none support");
                        return ScopeType.None;
                    case ScopeType.Property:
                        Invalidate("Property none support");
                        return ScopeType.None;
                    case ScopeType.Method:
                        Invalidate("Method none support");
                        return ScopeType.None;
                    case ScopeType.None:
                        Invalidate("Unknown expression");
                        return ScopeType.None;
                    default:
                        break;
                }

                // Set properties
                switch (NodeType)
                {
                    case NodeType.Using:
                    case NodeType.Namespace:
                    case NodeType.Interface:
                    case NodeType.Class:
                        if (NameCount == 1)
                        {
                            CSharpToken typeNameToken = Tokens[NameIndex] as CSharpToken;
                            if (typeNameToken.TokenType == CSharpTokenType.Name || typeNameToken.TokenType == CSharpTokenType.TypeName)
                            {
                                typeNameToken.TokenType = CSharpTokenType.TypeName;
                                Name = typeNameToken.Text;
                            }
                            else
                            {
                                Invalidate("Unknown error");
                                return ScopeType.None;
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
                            if ((typeNameToken.TokenType == CSharpTokenType.Name || typeNameToken.TokenType == CSharpTokenType.TypeName) && nameToken.TokenType == CSharpTokenType.Name)
                            {
                                typeNameToken.TokenType = CSharpTokenType.TypeName;
                                TypeName = typeNameToken.Text;
                                if (nameToken.TokenType == CSharpTokenType.Name)
                                {
                                    Name = nameToken.Text;
                                }
                                else
                                {
                                    Invalidate("No name specified");
                                    return ScopeType.None;
                                }
                            }
                            else
                            {
                                Invalidate("Type not specified");
                                return ScopeType.None;
                            }
                        }
                        break;
                    case NodeType.Root:
                    case NodeType.Scope:
                    case NodeType.Params:
                        break;
                    case NodeType.None:
                    default:
                        Invalidate("Unknown expression");
                        return ScopeType.None;
                }
            }
            return NodeType switch
            {
                NodeType.None => ScopeType.None,
                NodeType.Root => ScopeType.Root,
                NodeType.Scope => scopeType,
                NodeType.Params => ScopeType.Params,
                NodeType.Using => ScopeType.None,
                NodeType.Namespace => ScopeType.Namespace,
                NodeType.Class => ScopeType.Class,
                NodeType.Property => ScopeType.Property,
                NodeType.Interface => ScopeType.Interface,
                NodeType.Event => ScopeType.Event,
                NodeType.Method => ScopeType.Method,
                NodeType.Var => ScopeType.None,
                _ => ScopeType.None,
            };
        }
        private void Invalidate()
        {
            foreach (CSharpToken token in Tokens)
            {
                token.TokenType = CSharpTokenType.None;
            }
            NodeType = NodeType.None;
        }
        private void Invalidate(string message)
        {
            foreach (CSharpToken token in Tokens)
            {
                token.TokenType = CSharpTokenType.None;
            }
            NodeType = NodeType.None;
            ErrorMessage = message;
        }
        private bool CheckRange(List<NodeType> nodeTypes)
        {
            foreach (var node in nodeTypes)
            {
                if (NodeType == node)
                {
                    return true;
                }
            }
            Invalidate($"{NodeType} cannot be declared here");
            return false;
        }
        private int DetermineAccessModificator()
        {
            if ((Tokens[0] as CSharpToken).TokenType == CSharpTokenType.AccessModifier)
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
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(NodeType);
            if (Tokens.Count > 0)
            {
                builder.Append(" ");
                for (int i = 0, n = Tokens.Count; i < n; i++)
                {
                    var token = Tokens[i];
                    builder.Append(token);
                    if (i < n - 1)
                        builder.Append(", ");
                }
            }
            if (SubNodes.Count > 0)
            {
                builder.Append("{");
                for (int i = 0, n = SubNodes.Count; i < n; i++)
                {
                    var node = SubNodes[i];
                    builder.Append(node);
                    if (i < n - 1)
                        builder.Append(", \n");
                }
                builder.Append("}");
            }
            return builder.ToString();
        }
    }
}
