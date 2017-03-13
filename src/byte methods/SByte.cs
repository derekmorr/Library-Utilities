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

namespace Landis.Utilities.ByteMethods
{
    public class SByte
        : IByteMethods<sbyte>
    {
        public SByte()
        {
        }

        //---------------------------------------------------------------------

        private byte[] MyToBytes(sbyte value)
        {
            byte[] result = new byte[] { (byte) value };
            return result;
        }

        //---------------------------------------------------------------------

        public ToBytesMethod<sbyte> ToBytes
        {
            get {
                return new ToBytesMethod<sbyte>(MyToBytes);
            }
        }

        //---------------------------------------------------------------------

        private sbyte MyFromBytes(byte[] value,
                                  int    startIndex)
        {
            return (sbyte) (value[startIndex]);
        }

        //---------------------------------------------------------------------

        public FromBytesMethod<sbyte> FromBytes
        {
            get {
                return new FromBytesMethod<sbyte>(MyFromBytes);
            }
        }
    }
}
