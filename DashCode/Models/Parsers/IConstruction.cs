using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentParser
{
    public interface IConstruction
    {
        public bool ParseBody(string text);
        public FormattedStrings GetFormattedStrings(string document);
    }
}
