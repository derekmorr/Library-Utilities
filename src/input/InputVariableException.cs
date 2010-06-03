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

using System;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// An exception that is thrown when reading an InputVariable.
    /// </summary>
    public class InputVariableException
        : MultiLineException
    {
        public readonly InputVariable Variable;

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable var,
                                      string        message)
            : base(message)
        {
            Variable = var;
        }

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable   var,
                                      string          message,
                                      params object[] args)
            : base(string.Format(message, args))
        {
            Variable = var;
        }

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable var,
                                      string        message,
                                      Exception     exception)
            : base(message, exception)
        {
            Variable = var;
        }
    }
}
