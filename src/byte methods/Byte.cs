// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// The copyright holder licenses this file under the New (3-clause) BSD 
// License (the "License").  You may not use this file except in 
// compliance with the License.  A copy of the License is available at 
// 
//   http://www.opensource.org/licenses/bsd-license.php 
// 
// and is included in the NOTICE.txt file distributed with this work.
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;

namespace Edu.Wisc.Forest.Flel.Util.ByteMethods
{
    public class Byte
        : IByteMethods<byte>
    {
        public Byte()
        {
        }

        //---------------------------------------------------------------------

        private byte[] MyToBytes(byte value)
        {
            byte[] result = new byte[] { value };
            return result;
        }

        //---------------------------------------------------------------------

        public ToBytesMethod<byte> ToBytes
        {
            get {
                return new ToBytesMethod<byte>(MyToBytes);
            }
        }

        //---------------------------------------------------------------------

        private byte MyFromBytes(byte[] value,
                                 int    startIndex)
        {
            return value[startIndex];
        }

        //---------------------------------------------------------------------

        public FromBytesMethod<byte> FromBytes
        {
            get {
                return new FromBytesMethod<byte>(MyFromBytes);
            }
        }
    }
}
