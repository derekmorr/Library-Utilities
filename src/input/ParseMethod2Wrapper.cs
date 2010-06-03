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

using System.Globalization;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Wrapper for a ParseMethod2 so it can used as a ParseMethod.
    /// </summary>
    public class ParseMethod2Wrapper<T, Parameter2Type>
    {
        private ParseMethod2<T, Parameter2Type> parseMethod;
        private Parameter2Type parameter2;

        //---------------------------------------------------------------------

        public ParseMethod2Wrapper(ParseMethod2<T, Parameter2Type> method,
                                   Parameter2Type                  parameter2)
        {
            this.parseMethod = method;
            this.parameter2 = parameter2;
        }

        //---------------------------------------------------------------------

        public T Parse(string s)
        {
            return parseMethod(s, parameter2);
        }
    }
}
