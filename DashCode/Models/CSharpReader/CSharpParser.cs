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

        public BaseNode MakeTree(List<Token> tokens, BaseNode node, ref int pos)
        {
            DeductionNode currentdeductionNode = new DeductionNode(new List<Token>());
            for (int i = pos; i < tokens.Count; i++)
            {
                CSharpToken token = tokens[i] as CSharpToken;
                switch (token.TokenType)
                {
                    case CSharpTokenType.Separator:
                        if (!currentdeductionNode.Check())
                            OutputLog.Add(currentdeductionNode.ErrorMessage);
                        node.AddNode(currentdeductionNode);
                        currentdeductionNode = new DeductionNode(new List<Token>());
                        break;
                    case CSharpTokenType.ScopeStart:
                        {
                            i++;
                            currentdeductionNode.AddNode(MakeTree(tokens, new ScopeNode(), ref i));
                            if (!currentdeductionNode.Check())
                                OutputLog.Add(currentdeductionNode.ErrorMessage);
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
                            if (!currentdeductionNode.Check())
                                OutputLog.Add(currentdeductionNode.ErrorMessage);
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
            OutputLog.Clear();
            int pos = 0;
            RootNode root = MakeTree(tokens, new RootNode(), ref pos) as RootNode;
            return root;
        }
    }
}
