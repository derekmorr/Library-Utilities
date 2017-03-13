// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System.Diagnostics;
using IO = System.IO;

namespace Landis.Utilities
{
    public class InputVar<T>
        : InputVariable
    {
        private InputValue<T> myValue;
        private ReadMethod<T> readMethod;
        private int index;

        //---------------------------------------------------------------------

        public InputValue<T> Value
        {
            get {
                return myValue;
            }
        }

        //---------------------------------------------------------------------

        public int Index
        {
            get {
                return index;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public InputVar(string        name,
                        ReadMethod<T> readMethod)
            : base(name)
        {
            myValue = null;
            Require.ArgumentNotNull(readMethod);
            this.readMethod = readMethod;
            index = -1;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with a parse method.
        /// </summary>
        /// <remarks>
        /// The variable's read method is constructed by wrapping the given
        /// parse method.
        /// </remarks>
        public InputVar(string         name,
                        ParseMethod<T> parseMethod)
            : this(name, new ParseMethodWrapper<T>(parseMethod).Read)
        {
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with the read method registered for
        /// type T.
        /// </summary>
        public InputVar(string name)
            : this(name, InputValues.GetReadMethod<T>())
        {
        }

        //---------------------------------------------------------------------

        public override void ReadValue(StringReader reader)
        {
            try {
                myValue = readMethod(reader, out index);
            }
            catch (System.Exception exception) {
                string message = string.Format("Error reading input value for {0}",
                                               Name);
                throw new InputVariableException(this, message, exception);
            }
        }
    }
}
