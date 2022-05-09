using DashCode.Models.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.CSharpProcessing
{
    public class CSharpParser : DocumentParser
    {
        public CSharpParser()
        {
            OutputLog = new List<string>();
        }

        public CSharpNode MakeTree(List<Token> tokens, CSharpNode node, ref int pos)
        {
            CSharpNode currentdeductionNode = new CSharpNode(new List<Token>());
            for (int i = pos; i < tokens.Count; i++)
            {
                CSharpToken token = tokens[i] as CSharpToken;
                switch (token.TokenType)
                {
                    case CSharpTokenType.Separator:
                        node.AddNode(currentdeductionNode);
                        currentdeductionNode = new CSharpNode(new List<Token>());
                        break;
                    case CSharpTokenType.ScopeStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new CSharpNode(NodeType.Scope), ref i));
                            node.AddNode(currentdeductionNode);
                            currentdeductionNode = new CSharpNode(new List<Token>());
                        }
                        break;
                    case CSharpTokenType.ParamsStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new CSharpNode(NodeType.Params), ref i));
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
        public override IConstruction Parse(List<Token> tokens)
        {
            int pos = 0;
            var root = MakeTree(tokens, new CSharpNode(NodeType.Root), ref pos);
            OutputLog = root.DetermineAll(ScopeType.Root);
            return root;
        }
    }
}
