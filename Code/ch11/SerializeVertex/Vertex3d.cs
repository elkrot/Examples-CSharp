using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerializeVertex
{
    [Serializable]
    public struct Vertex3d : ISerializable
    {
        private int? _id ;

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

        public Vertex3d(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;

            _id = null;
        }

        //serialization
        private Vertex3d(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            int tempId = info.GetInt32("id");
            if (tempId != 0)
                _id = tempId;
            else
                _id = null;
            _x = info.GetInt32("x");
            _y = info.GetInt32("y");
            _z = info.GetInt32("z");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", _id.HasValue ? _id.Value : 0);
            info.AddValue("x", _x);
            info.AddValue("y", _y);
            info.AddValue("z", _z);            
        }
    }
}
