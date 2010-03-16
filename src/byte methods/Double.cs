using System;

namespace Edu.Wisc.Forest.Flel.Util.ByteMethods
{
    public class Double
        : IByteMethods<double>
    {
        public Double()
        {
        }

        //---------------------------------------------------------------------

        public ToBytesMethod<double> ToBytes
        {
            get {
                return new ToBytesMethod<double>(BitConverter.GetBytes);
            }
        }

        //---------------------------------------------------------------------

        public FromBytesMethod<double> FromBytes
        {
            get {
                return new FromBytesMethod<double>(BitConverter.ToDouble);
            }
        }
    }
}
