using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentParser
{
    public interface ILexeme
    {
        public bool CheckInterval(int pos, int len);
        public int Position { get; }
        public int TotalLength { get; }
        public string Text { get; }
    }
}
