using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders.CSharp
{
    public class CSharpParser : IDocumentParser
    {
        /*switch (token.TokenType)
                {
                    case CSharpTokenType.Separator:
                        node.AddNode(new DeductionNode(curentTokenSet));
                        curentTokenSet = new List<Token>();
                        break;
                    case CSharpTokenType.ScopeStart:
                        if (curentTokenSet.Count != 0)
                        {
                            node.AddNode(new DeductionNode(curentTokenSet));
                            curentTokenSet = new List<Token>();
                        }
                        i++;
                        node.AddNode(MakeTree(tokens, new ScopeNode(), ref i));
                        break;
                    case CSharpTokenType.ScopeEnd:
                        if (curentTokenSet.Count != 0)
                            node.AddNode(new DeductionNode(curentTokenSet));
                        pos = i;
                        node.Check();
                        return node;
                    case CSharpTokenType.ParamsStart:
                        if (curentTokenSet.Count != 0)
                        {
                            node.AddNode(new DeductionNode(curentTokenSet));
                            curentTokenSet = new List<Token>();
                        }
                        i++;
                        node.AddNode(MakeTree(tokens, new ParamsNode(), ref i));
                        break;
                    case CSharpTokenType.ParamsEnd:
                        if (curentTokenSet.Count != 0)
                            node.AddNode(new DeductionNode(curentTokenSet));
                        pos = i;
                        node.Check();
                        return node;
                    default:
                        curentTokenSet.Add(token);
                        break;
                }*/
        public BaseNode MakeTree(List<Token> tokens, BaseNode node, ref int pos)
        {
            DeductionNode currentdeductionNode = new DeductionNode(new List<Token>());
            for (int i = pos; i < tokens.Count; i++)
            {
                CSharpToken token = tokens[i] as CSharpToken;
                switch (token.TokenType)
                {
                    case CSharpTokenType.Separator:
                        currentdeductionNode.Check();
                        node.AddNode(currentdeductionNode);
                        currentdeductionNode = new DeductionNode(new List<Token>());
                        break;
                    case CSharpTokenType.ScopeStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new ScopeNode(), ref i));
                            currentdeductionNode.Check();
                            node.AddNode(currentdeductionNode);
                            currentdeductionNode = new DeductionNode(new List<Token>());
                        }
                        break;
                    case CSharpTokenType.ParamsStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new ParamsNode(), ref i));
                            //node.AddNode(currentdeductionNode);
                        }
                        break;
                    case CSharpTokenType.ParamsEnd:
                    case CSharpTokenType.ScopeEnd:
                        if (currentdeductionNode.Tokens.Count != 0 || currentdeductionNode.SubNodes.Count != 0)
                        {
                            node.AddNode(currentdeductionNode);
                        }
                        pos = i;
                        return node;
                    default:
                        currentdeductionNode.Tokens.Add(token);
                        break;
                }
            }
            return node;
        }
        public IConstruction Parse(List<Token> tokens)
        {
            int pos = 0;
            RootNode root = MakeTree(tokens, new RootNode(), ref pos) as RootNode;
            return root;
        }
    }
}
