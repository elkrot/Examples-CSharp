using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ComplexNumberDemo
{
    class ComplexFormatter : IFormatProvider, ICustomFormatter
    {
        //accepts two format specifiers: i and j
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is Complex)
            {
                Complex c = (Complex)arg;
                if (format.Equals("i", StringComparison.OrdinalIgnoreCase))
                {
                    return c.Real.ToString("N2") + " + " + c.Imaginary.ToString("N2") + "i";
                }
                else if (format.Equals("j", StringComparison.OrdinalIgnoreCase))
                { 
                    return c.Real.ToString("N2") + " + " + c.Imaginary.ToString("N2") + "j"; 
                }
                else 
                { 
                    return c.ToString(format, formatProvider); 
                }
            }
            else
            {
                if (arg is IFormattable)
                {
                    return ((IFormattable)arg).ToString(format, formatProvider);
                }
                else if (arg != null)
                {
                    return arg.ToString();
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
            }
        }
    }
}
