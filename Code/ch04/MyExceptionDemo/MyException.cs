using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MyExceptionDemo
{
    [Serializable]
    public class MyException : Exception, ISerializable
    {
        private double _exceptionData = 0.0;

        public double ExceptionData
        {
            get { return _exceptionData; }
        }

        public MyException()
        {
        }

        public MyException(string message)
            :base(message)
        {
        }

        public MyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MyException(double exceptionData, string message)
            :base(message)
        {
            _exceptionData = exceptionData;
        }

        public MyException(double exceptionData, string message, Exception innerException)
            :base(message, innerException)
        {
            _exceptionData = exceptionData;
        }

        //serialization handlers
        protected MyException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            //deserialize
            _exceptionData = info.GetDouble("MyExceptionData");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //serialize
            base.GetObjectData(info, context);
            info.AddValue("MyExceptionData", _exceptionData);
        }
    }
}
