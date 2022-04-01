using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public class Token
    {
        public Token(string text)
        {
            Text = text;
        }
        public virtual bool Check()
        {
            return true;
        }
        public int TotalLength => Text.Length;
        public string Text { get; }
    }
}
