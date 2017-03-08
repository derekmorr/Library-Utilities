// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;
using IO = System.IO;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Methods for working with directories.
    /// </summary>
    public static class Directory
    {
        /// <summary>
        /// Ensures that a directory exists, creating it if it doesn't.
        /// </summary>
        /// <param name="path">
        /// Path to the directory.
        /// </param>
        /// <exception cref="System.ApplicationException">
        /// The path refers to a file.
        /// </exception>
        /// <exception cref="MultiLineException">
        /// An exception occurred when trying to create the directory.  That
        /// exception is the InnerException of the exception thrown by this
        /// method.
        /// </exception>
        /// <remarks>
        /// Any parent directories in the path that do not exist are also
        /// created.
        /// </remarks>
        public static void EnsureExists(string path)
        {
            if (IO.Directory.Exists(path))
                return;

            if (IO.File.Exists(path)) {
                string mesg = string.Format("{0} is a file, not a directory.", path);
                throw new ApplicationException(mesg);
            }

            try {
                IO.DirectoryInfo info = IO.Directory.CreateDirectory(path);
            }
            catch (System.Exception exc) {
                string mesg = string.Format("Could not create the directory \"{0}\"", path);
                throw new MultiLineException(mesg, exc);
            }
        }
    }
}
