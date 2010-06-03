// Copyright 2005 University of Wisconsin 
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
    /// A parser that reads input from a LineReader and returns an instance
    /// of T.
    /// </summary>
    public interface ITextParser<T>
    {
        /// <summary>
        /// Parses the input from a LineReader and constructs an instance of T.
        /// </summary>
        T Parse(LineReader reader);
    }
}
