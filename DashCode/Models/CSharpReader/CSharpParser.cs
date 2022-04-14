using DashCode.Models.DocumentReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.CSharpReader
{
    public class CSharpParser : IDocumentParser
    {
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
