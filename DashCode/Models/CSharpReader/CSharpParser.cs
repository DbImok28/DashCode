using DashCode.Models.DocumentReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.CSharpReader
{
    public class CSharpParser : DocumentParser
    {
        public CSharpParser()
        {
            OutputLog = new List<string>();
        }

        public DeductionNode MakeTree(List<Token> tokens, DeductionNode node, ref int pos)
        {
            DeductionNode currentdeductionNode = new DeductionNode(new List<Token>());
            for (int i = pos; i < tokens.Count; i++)
            {
                CSharpToken token = tokens[i] as CSharpToken;
                switch (token.TokenType)
                {
                    case CSharpTokenType.Separator:
                        node.AddNode(currentdeductionNode);
                        currentdeductionNode = new DeductionNode(new List<Token>());
                        break;
                    case CSharpTokenType.ScopeStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new DeductionNode(NodeType.Scope), ref i));
                            node.AddNode(currentdeductionNode);
                            currentdeductionNode = new DeductionNode(new List<Token>());
                        }
                        break;
                    case CSharpTokenType.ParamsStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new DeductionNode(NodeType.Params), ref i));
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
        public override IConstruction Parse(List<Token> tokens)
        {
            int pos = 0;
            var root = MakeTree(tokens, new DeductionNode(NodeType.Root), ref pos);
            OutputLog = root.DetermineAll(ScopeType.Root);
            return root;
        }
    }
}
