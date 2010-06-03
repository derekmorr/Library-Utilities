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
    /// A method to parse a string for an input value of a specific type.
    /// </summary>
    public delegate T ParseMethod<T>(string str);

    //-------------------------------------------------------------------------

    /// <summary>
    /// A 2-parameter method to parse a string for an input value of a specific
    /// type.
    /// </summary>
    public delegate T ParseMethod2<T, Parameter2Type>(string         str,
                                                      Parameter2Type parameter2);
}
