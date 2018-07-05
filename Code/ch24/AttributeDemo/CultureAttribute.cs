using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeDemo
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    class CultureAttribute : Attribute
    {
        string _culture;

        public string Culture
        {
            get
            {
                return _culture;
            }
        }

        public CultureAttribute(string culture)
        {
            _culture = culture;
        }
    }
}
