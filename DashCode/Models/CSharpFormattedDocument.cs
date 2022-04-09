using DashCode.Models.DocumentReaders.CSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace DashCode.Models
{
    public class CSharpFormattedDocument : FormattedEditorDocument
    {
        public CSharpFormattedDocument(EditorDocument editorDocument) : base(editorDocument)
        {
        }

        public override void Format()
        {
            var rawDocument = EditorDocument.RawDocument;
            int pos = 0;
            FormattedStrings result = new FormattedStrings(new List<FormattedString>());
            foreach(DeductionNode node in (EditorDocument.ReadedDocument as RootNode).SubNodes)
            {
                result.Add(Format(rawDocument, ref pos, node));
            }
            string noFormattedText = "";
            for (; pos < rawDocument.Length; pos++)
            {
                noFormattedText += rawDocument[pos];
            }
            result.Add(new FormattedString(noFormattedText, NoFormattedTextColor));
            Document = result;
        }
        public FormattedStrings Format(string doc, ref int pos, DeductionNode node)
        {
            FormattedStrings result = new FormattedStrings(new List<FormattedString>());
            foreach (CSharpToken token in node.Tokens)
            {
                string noFormattedText = "";
                Color color = ErrorTextColor;
                if (!ColorsDictionary.TryGetValue(token.TokenType, out color))
                {
                    color = ErrorTextColor;
                }
                bool isFinded = false;
                for (; pos < doc.Length; pos++)
                {
                    if (doc[pos] == token.Text[0])
                    {
                        isFinded = true;
                        int nextPos = pos;
                        for (int j = 0; j < token.TotalLength; j++, nextPos++)
                        {
                            //noFormattedText += doc[pos];
                            if (doc[nextPos] != token.Text[j])
                            {
                                isFinded = false;
                                break;
                            }
                        }
                        if (isFinded)
                        {
                            result.Add(new FormattedString(noFormattedText, NoFormattedTextColor));
                            result.Add(new FormattedString(token.Text, color));
                            pos = nextPos;
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < pos - nextPos; i++)
                            {
                                noFormattedText += doc[i];
                            }
                            pos = nextPos;
                        }
                    }
                    noFormattedText += doc[pos];
                }
                if (!isFinded)
                {
                    result.Add(new FormattedString(noFormattedText, ErrorTextColor));
                }
            }
            foreach (var subNode in node.SubNodes)
            {
                if (subNode is DeductionNode deductionNode)
                {
                    result.Add(Format(doc, ref pos, deductionNode));
                }
                else if (subNode is ScopeNode scopeNode)
                {
                    foreach (DeductionNode scopeSubNode in scopeNode.SubNodes)
                    {
                        result.Add(Format(doc, ref pos, scopeSubNode));
                    }
                }
                else if (subNode is ParamsNode paramNode)
                {
                    foreach (DeductionNode paramSubNode in paramNode.SubNodes)
                    {
                        result.Add(Format(doc, ref pos, paramSubNode));
                    }
                }
            }
            return result;
        }
        public static Color NoFormattedTextColor = Color.FromRgb(0, 0, 0);
        public static Color ErrorTextColor = Color.FromRgb(255, 0, 0);
        public static Dictionary<CSharpTokenType, Color> ColorsDictionary = new Dictionary<CSharpTokenType, Color>(){
            {CSharpTokenType.AccessModifier, Color.FromRgb(10,10,250)},
            {CSharpTokenType.KeyName, Color.FromRgb(10,10,250)},
            {CSharpTokenType.Name, Color.FromRgb(4, 205, 224)},
            {CSharpTokenType.TypeName, Color.FromRgb(9, 222, 133)},
            {CSharpTokenType.None, Color.FromRgb(255,0,0)},
        };
    }
}
