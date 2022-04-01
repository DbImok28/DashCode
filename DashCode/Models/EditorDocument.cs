using DashCode.Models.DocumentParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;

namespace DashCode.Models
{
    public class EditorDocument
    {
        public EditorDocument(string rawDocument, IDocumentParser parser)
        {
            if (string.IsNullOrWhiteSpace(rawDocument))
            {
                throw new ArgumentException($"'{nameof(rawDocument)}' cannot be null or whitespace.", nameof(rawDocument));
            }

            RawDocument = rawDocument;
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));

            //Test
            Color keyNamesColor = Color.FromRgb(0, 0, 255);
            Color variableColor = Color.FromRgb(191, 141, 80);
            Color defaultColor = Color.FromRgb(0, 0, 0);
            FormattedDocument = new FormattedStrings(new List<FormattedString>{
                    new FormattedString("int", keyNamesColor),
                    new FormattedString(" ", defaultColor),
                    new FormattedString("Count", variableColor),
                    new FormattedString(";", defaultColor),
                    new FormattedString("\n", defaultColor),
                    new FormattedString("double", keyNamesColor),
                    new FormattedString(" ", defaultColor),
                    new FormattedString("Value", variableColor),
                    new FormattedString(";", defaultColor)
                }
            );
        }
            
        public string RawDocument { get; private set; }
        public FormattedStrings FormattedDocument { get; private set; }
        public IDocumentParser Parser { get; private set; }
        public void AddText(int pos, string str)
        {
            RawDocument.Insert(pos, str);
            UpadateFormatted();
        }
        public void RemoveText(int pos, int length)
        {
            RawDocument.Remove(pos, length);
            UpadateFormatted();
        }
        public void SetText(string document)
        {
            RawDocument = document;
            UpadateFormatted();
        }
        public void UpadateFormatted()
        {

        }
        public void Parse()
        {
            Parser.ParseDocument(RawDocument);
        }
    }
}
