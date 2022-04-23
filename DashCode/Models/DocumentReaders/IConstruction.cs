using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DocumentReaders
{
    public interface IConstruction
    {
        public bool CheckInterval(int pos, int len, int iterPos);
    }
}
