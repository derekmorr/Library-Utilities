// Copyright 2005 University of Wisconsin 
// All rights reserved. 
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
