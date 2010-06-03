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
    /// Interface to an editable version of a class.
    /// </summary>
    public interface IEditable<T>
    {
        /// <summary>
        /// Has all the necessary data been specified so that a complete
        /// instance of T can be generated?
        /// </summary>
        bool IsComplete
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts an instance to a complete instance of T.
        /// </summary>
        /// <returns>
        /// null if IsComplete is false.
        /// </returns>
        T GetComplete();
    }
}
