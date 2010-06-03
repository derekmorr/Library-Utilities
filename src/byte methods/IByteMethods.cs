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

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// A method for converting a value into a sequence of bytes.
    /// </summary>
    public delegate byte[] ToBytesMethod<T>(T value);

    //-------------------------------------------------------------------------

    /// <summary>
    /// A method for converting a sequence of bytes into a value.
    /// </summary>
    public delegate T FromBytesMethod<T>(byte[] bytes,
                                         int    index);

    //-------------------------------------------------------------------------

    /// <summary>
    /// Set of methods for converting the values of a type into byte sequences.
    /// </summary>
    public interface IByteMethods<T>
    {
        ToBytesMethod<T> ToBytes
        {
            get;
        }

        FromBytesMethod<T> FromBytes
        {
            get;
        }
    }
}
