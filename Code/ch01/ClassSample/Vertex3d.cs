using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassSample
{
    public class Vertex3d 
    {
        private static int _numInstances = 0;
        
        private double _x;
        private double _y;
        private double _z;

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

        public void SetToOrigin()
        {
            X = Y = Z = 0.0;
        }

        public Vertex3d()
        {
            _x = _y = _z = 0.0;
        }

        public Vertex3d(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vertex3d(System.Drawing.Point point)
            :this(point.X, point.Y, 0)
        {
        }

        public static Vertex3d Add(Vertex3d a, Vertex3d b)
        {
            Vertex3d result = new Vertex3d();
            result.X = a.X + b.X;
            result.Y= a.Y + b.Y;
            result.Z = a.Z + b.Z;
            return result;
        }
    }
}
