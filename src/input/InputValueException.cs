// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

namespace Landis.Utilities
{
    public class InputValueException
        : MultiLineException
    {
        private string val;

        //---------------------------------------------------------------------

        public string Value
        {
            get {
                return val;
            }
        }

        //---------------------------------------------------------------------

        public InputValueException(string inputValue,
                                   string message,
                                   params object[] messageArgs)
            : base(string.Format(message, messageArgs))
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        public InputValueException(string        inputValue,
                                   string        message,
                                   MultiLineText innerMessage)
            : base(message, innerMessage)
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        public InputValueException(string        inputValue,
                                   MultiLineText  message,
                                   MultiLineText innerMessage)
            : base(message, innerMessage)
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        public InputValueException(string        inputValue,
                                   MultiLineText message)
            : base(message)
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        //  This ctor is needed because if a ctor invocation with 2 string
        //  parameters does not call the ctor above (with MutliLineText as
        //  the 2nd parameter), but rather calls the ctor whose 3rd parameter
        //  is "params object[] mesgArgs" with a zero-length array.  That is
        //  potentially problematic if the message contains text that may
        //  look like formats, e.g., "Missing the template variable {foo}".
        public InputValueException(string inputValue,
                                   string message)
            : base(message)
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with a particular message that has a
        /// single string argument.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="message"></param>
        /// <param name="messageArg"></param>
        /// <remarks>
        /// This constructor is a special case of the more general constructor
        /// that takes a message and message arguments.  This special case is
        /// explicitly defined because if it wasn't, the constructor that takes
        /// a MultiLineText inner message would be called (because of the
        /// implicit conversion operator from string to MultiLineText).
        /// </remarks>
        public InputValueException(string inputValue,
                                   string message,
                                   string messageArg)
            : base(string.Format(message, messageArg))
        {
            this.val = inputValue;
        }

        //---------------------------------------------------------------------

        public InputValueException()
            : base("Missing value")
        {
            this.val = null;
        }
    }
}
