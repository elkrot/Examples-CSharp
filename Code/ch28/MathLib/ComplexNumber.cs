using System;

namespace MathLib
{
    public struct ComplexNumber
    {
        private double _real;
        private double _imaginary;

        public double RealPart 
        {
            get { return _real; } 
            set { _real = value; } 
        }
        public double ImaginaryPart 
        {
            get { return _imaginary; } 
            set { _imaginary = value; } 
        }
        
        public ComplexNumber(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public static ComplexNumber operator*(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber((a.RealPart * b.RealPart) - (a.ImaginaryPart * b.ImaginaryPart),
                (a.RealPart * b.ImaginaryPart) + (b.RealPart * a.ImaginaryPart));
        }
    }
}
