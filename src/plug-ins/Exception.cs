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

namespace Landis.Utilities.PlugIns
{
    /// <summary>
    /// An exception related to plug-in components.
    /// </summary>
    public class Exception
        : MultiLineException
    {
        private IInfo plugIn;

        //---------------------------------------------------------------------

        /// <summary>
        /// The plug-in that the exception relates to.
        /// </summary>
        public IInfo PlugIn
        {
            get {
                return plugIn;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="plugIn">
        /// Information about the plug-in that the exception relates to.
        /// </param>
        /// <param name="message">
        /// A single-line message describing the exception.
        /// </param>
        public Exception(IInfo  plugIn,
                         string message)
            : base(message)
        {
            this.plugIn = plugIn;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="plugIn">
        /// Information about the plug-in that the exception relates to.
        /// </param>
        /// <param name="message">
        /// A single-line message describing the exception.
        /// </param>
        /// <param name="innerMessage">
        /// Additional details about the exception.
        /// </param>
        public Exception(IInfo         plugIn,
                         string        message,
                         MultiLineText innerMessage)
            : base(message, innerMessage)
        {
            this.plugIn = plugIn;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="plugIn">
        /// Information about the plug-in that the exception relates to.
        /// </param>
        /// <param name="message">
        /// A single-line message describing the exception.  The message may
        /// refer to arguments, e.g., "{0}".
        /// </param>
        /// <param name="messageArgs">
        /// Arguments to be inserted into the message.
        /// </param>
        public Exception(IInfo           plugIn,
                         string          message,
                         params object[] messageArgs)
            : base(string.Format(message, messageArgs))
        {
            this.plugIn = plugIn;
        }
    }
}
