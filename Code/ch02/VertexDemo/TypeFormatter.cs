using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VertexDemo
{
    class TypeFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string value;
            IFormattable formattable = arg as IFormattable;
            if (formattable == null)
            {
                value = arg.ToString();
            }
            else
            {
                value = formattable.ToString(format, formatProvider);
            }

            return string.Format("Type: {0}, Value: {1}", arg.GetType(), value);            
        }
    }
}
