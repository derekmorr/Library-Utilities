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
    public class Float
        : IByteMethods<float>
    {
        public Float()
        {
        }

        public ToBytesMethod<float> ToBytes
        {
            get {
                return new ToBytesMethod<float>(BitConverter.GetBytes);
            }
        }

        public FromBytesMethod<float> FromBytes
        {
            get {
                return new FromBytesMethod<float>(BitConverter.ToSingle);
            }
        }
    }
}
