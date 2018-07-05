using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumbersToBytes
{
    class NumberToBytes
    {
        static void Main(string[] args)
        {
            const Int16 val = 13;

            byte[] int16Bytes = BitConverter.GetBytes((Int16)val);
            byte[] int32Bytes = BitConverter.GetBytes((Int32)val);
            byte[] int64Bytes = BitConverter.GetBytes((Int64)val);
            byte[] floatBytes = BitConverter.GetBytes((float)val);
            byte[] doubleBytes = BitConverter.GetBytes((double)val);
            byte[] decimalBytes = DecimalToBytes(val);

            Console.WriteLine("Int16: {0} ==> {1}", val, GetByteString(int16Bytes));    
            Console.WriteLine("Int32: {0} ==> {1}", val, GetByteString(int32Bytes));
            Console.WriteLine("Int64: {0} ==> {1}", val, GetByteString(int64Bytes));
            Console.WriteLine("float: {0} ==> {1}", val, GetByteString(floatBytes));
            Console.WriteLine("double: {0} ==> {1}", val, GetByteString(doubleBytes));
            Console.WriteLine("decimal: {0} ==> {1}", val, GetByteString(decimalBytes));

            Int16 int16Decoded = BitConverter.ToInt16(int16Bytes, 0);
            Int32 int32Decoded = BitConverter.ToInt32(int32Bytes, 0);
            Int64 int64Decoded = BitConverter.ToInt64(int64Bytes, 0);
            float floatDecoded = BitConverter.ToSingle(floatBytes, 0);
            double doubleDecoded = BitConverter.ToDouble(doubleBytes, 0);
            decimal decimalDecoded = BytesToDecimal(decimalBytes);

            Console.WriteLine("Int16: {0} ==> {1}", GetByteString(int16Bytes), int16Decoded);
            Console.WriteLine("Int32: {0} ==> {1}", GetByteString(int32Bytes), int32Decoded);
            Console.WriteLine("Int64: {0} ==> {1}", GetByteString(int64Bytes), int64Decoded);
            Console.WriteLine("float: {0} ==> {1}", GetByteString(floatBytes), floatDecoded);
            Console.WriteLine("double: {0} ==> {1}", GetByteString(doubleBytes), doubleDecoded);
            Console.WriteLine("decimal: {0} ==> {1}", GetByteString(decimalBytes), decimalDecoded);
                                               
            Console.ReadKey();
        }

        static string GetByteString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder("0x");
            //go in reverse because bytes are in little-endian order
            for (int i=bytes.Length-1; i>=0; i--)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static byte[] DecimalToBytes(decimal number)
        {
            Int32[] bits = Decimal.GetBits(number);
            byte[] bytes = new byte[16];
            for (int i = 0; i < 4; i++)
            {
                bytes[i * 4 + 0] = (byte)(bits[i] & 0xFF);
                bytes[i * 4 + 1] = (byte)((bits[i] >> 0x8) & 0xFF);
                bytes[i * 4 + 2] = (byte)((bits[i] >> 0x10) & 0xFF);
                bytes[i * 4 + 3] = (byte)((bits[i] >> 0x18) & 0xFF);
            }
            return bytes;
        }

        static Decimal BytesToDecimal(byte[] bytes)
        {
            Int32[] bits = new Int32[4];
            for (int i = 0; i < 4; i++)
            {
                bits[i] =   bytes[i * 4 + 0];
                bits[i] |= (bytes[i * 4 + 1] << 0x8);
                bits[i] |= (bytes[i * 4 + 2] << 0x10);
                bits[i] |= (bytes[i * 4 + 3] << 0x18);
            }
            return new Decimal(bits);
        }

    }
}
