using System;

namespace Edu.Wisc.Forest.Flel.Util.ByteMethods
{
    public class UInt
        : IByteMethods<uint>
    {
        public UInt()
        {
        }

        //---------------------------------------------------------------------

        public ToBytesMethod<uint> ToBytes
        {
            get {
                return new ToBytesMethod<uint>(BitConverter.GetBytes);
            }
        }

        //---------------------------------------------------------------------

        public FromBytesMethod<uint> FromBytes
        {
            get {
                return new FromBytesMethod<uint>(BitConverter.ToUInt32);
            }
        }
    }
}
