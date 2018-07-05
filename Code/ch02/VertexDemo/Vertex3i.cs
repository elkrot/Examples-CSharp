using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VertexDemo
{
    struct Vertex3i
    {
        private int _id;

        private int _x;
        private int _y;
        private int _z;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public Vertex3i(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;

            _id = 0;
        }

        public override string ToString()
        {
            return ToString("G", null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "G";

            if (formatProvider != null)
            {
                ICustomFormatter formatter = formatProvider.GetFormat(this.GetType())
                    as ICustomFormatter;
                if (formatter != null)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            if (format == "G")
            {
                return string.Format("({0}, {1}, {2})", X, Y, Z);
            }

            StringBuilder sb = new StringBuilder();
            int sourceIndex = 0;

            while (sourceIndex < format.Length)
            {
                switch (format[sourceIndex])
                {
                    case 'X':
                        sb.Append(X.ToString());
                        break;
                    case 'Y':
                        sb.Append(Y.ToString());
                        break;
                    case 'Z':
                        sb.Append(Z.ToString());
                        break;
                    default:
                        sb.Append(format[sourceIndex]);
                        break;
                }
                sourceIndex++;
            }
            return sb.ToString();
        }

        public static implicit operator Vertex3d(Vertex3i vertex)
        {
            return new Vertex3d(vertex._x, vertex._y, vertex._z);
        }
    }
}
