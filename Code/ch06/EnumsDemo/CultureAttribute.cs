using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumsDemo
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
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

    static class CultureExtensions
    {
        public static string[] GetCultures(this BookLanguage language)
        {
            //note: this will only work for single-value genres
            CultureAttribute[] attributes = (CultureAttribute[])language.GetType().GetField(language.ToString()).GetCustomAttributes(typeof(CultureAttribute), false);
            string[] cultures = new string[attributes.Length];
            for (int i = 0; i < attributes.Length; i++)
            {
                cultures[i] = attributes[i].Culture;
            }
            return cultures;
        }
    }
}
