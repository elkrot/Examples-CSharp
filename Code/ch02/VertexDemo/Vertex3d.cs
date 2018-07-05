using System;
using System.Collections.Generic;
using System.Text;

namespace VertexDemo
{
    struct Vertex3d : IFormattable, IEquatable<Vertex3d>, IComparable<Vertex3d>
    {
        private int? _id;

        private double _x;
        private double _y;
        private double _z;

        public int? Id
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

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return _x;
                    case 1: return _y;
                    case 2: return _z;
                    default: throw new ArgumentOutOfRangeException("index", 
                        "Only indexes 0-2 valid!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: _x = value; break;
                    case 1: _y = value; break;
                    case 2: _z = value; break;
                    default: throw new ArgumentOutOfRangeException("index", 
                        "Only indexes 0-2 valid!");
                }
            }
        }

        public double this[string dimension]
        {
            get
            {
                switch (dimension)
                {
                    case "x": 
                    case "X": return _x;
                    case "y":
                    case "Y": return _y;
                    case "z":
                    case "Z": return _z;
                    default: throw new ArgumentOutOfRangeException("dimension", 
                        "Only dimensions X, Y, and Z are valid!");
                }
            }
            set
            {
                switch (dimension)
                {
                    case "x":
                    case "X": _x = value; break;
                    case "y":
                    case "Y": _y = value; break;
                    case "z":
                    case "Z": _z = value; break;
                    default: throw new ArgumentOutOfRangeException("dimension", 
                        "Only dimensions X, Y, and Z are valid!");
                }

            }
        }


        public Vertex3d(double x, double y, double z)
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Vertex3d)obj);
        }

        public bool Equals(Vertex3d other)
        {
            /* If Vertex3d were a reference type you would also need:
             * 
             * if ((object)other == null)
             *  return false;
             *  
             * if (!base.Equals(other))
             *  return false;
             */

            return this._x == other._x
                && this._y == other._y
                && this._z == other._z;
        }

        public override int GetHashCode()
        {
            return (((int)_x ^ (int)_z) << 16) |
                   (((int)_y ^ (int)_z) & 0x0000FFFF);
        }

        public int CompareTo(Vertex3d other)
        {
            if (_id < other._id)
                return -1;
            if (_id == other._id)
                return 0;
            return 1;
            /* We could also just do this:
             * return _id.CompareTo(other._id);
             * */
        }

        public static Vertex3d operator +(Vertex3d a, Vertex3d b)
        {
            return new Vertex3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static bool operator ==(Vertex3d a, Vertex3d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vertex3d a, Vertex3d b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vertex3i(Vertex3d vertex)
        {
            return new Vertex3i((Int32)vertex._x, (Int32)vertex._y, (Int32)vertex._z);
        }

        

    }
}
