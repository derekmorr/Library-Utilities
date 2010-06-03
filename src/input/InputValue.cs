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
    /// Input value.
    /// </summary>
    public class InputValue<T>
    {
        private T actualValue;
        private string valueAsStr;

        //---------------------------------------------------------------------

        /// <summary>
        /// The actual input value.
        /// </summary>
        public T Actual
        {
            get {
                return actualValue;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The string representation of the input value (usually entered by
        /// user).
        /// </summary>
        public string String
        {
            get {
                return valueAsStr;
            }
        }

        //---------------------------------------------------------------------

        public InputValue(T      actualValue,
                          string valueAsStr)
        {
            this.actualValue = actualValue;
            this.valueAsStr = valueAsStr;
        }

        //---------------------------------------------------------------------

        public override string ToString()
        {
            return valueAsStr;
        }

        //---------------------------------------------------------------------

        public static implicit operator T(InputValue<T> inputValue)
        {
            return inputValue.Actual;
        }
    }
}
