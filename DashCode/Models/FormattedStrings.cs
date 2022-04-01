using System;
using System.Collections;
using System.Collections.Generic;

namespace DashCode.Models
{
    public class FormattedStrings : IEnumerable<FormattedString>
    {
        public List<FormattedString> Strings { get; set; }
        public FormattedStrings(IEnumerable<FormattedString> strings)
        {
            Strings = new List<FormattedString>();
            Strings.AddRange(strings);
        }
        public void Add(FormattedString str)
        {
            Strings.Add(str);
        }
        public bool Remove(FormattedString str)
        {
            return Strings.Remove(str);
        }
        public bool Replace(FormattedString oldString, FormattedString newString)
        {
            int index = Strings.FindIndex(s => s == oldString);
            if (index != -1)
            {
                Strings[index] = newString;
                return true;
            }
            return false;
        }

        public IEnumerator<FormattedString> GetEnumerator()
        {
            return Strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Strings.GetEnumerator();
        }
    }
}
