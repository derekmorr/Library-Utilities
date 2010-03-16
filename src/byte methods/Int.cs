using System;

namespace Edu.Wisc.Forest.Flel.Util.ByteMethods
{
    public class Int
        : IByteMethods<int>
    {
        public Int()
        {
        }

        //---------------------------------------------------------------------

        public ToBytesMethod<int> ToBytes
        {
            get {
                return new ToBytesMethod<int>(BitConverter.GetBytes);
            }
        }

        //---------------------------------------------------------------------

        public FromBytesMethod<int> FromBytes
        {
            get {
                return new FromBytesMethod<int>(BitConverter.ToInt32);
            }
        }
    }
}
