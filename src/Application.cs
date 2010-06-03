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

using System;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Information about the current  application.
    /// </summary>
    public static class Application
    {
        private static string baseDir;

        //---------------------------------------------------------------------

        static Application()
        {
            baseDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The directory where the application is located.
        /// </summary>
        public static string Directory
        {
            get {
                return baseDir;
            }
        }
    }
}
