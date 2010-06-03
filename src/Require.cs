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
    /// Methods for testing assertions.
    /// </summary>
    public static class Require
    {
        /// <summary>
        /// Checks if an argument is not null.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Argument is null.
        /// </exception>
        public static void ArgumentNotNull(object arg)
        {
            if (arg == null)
                throw new System.ArgumentNullException();
        }
    }
}
