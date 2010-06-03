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

using System.Diagnostics;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Base class for parsers that parse text input.
    /// </summary>
    public abstract class TextParser<T>
        : ITextParser<T>
    {
        private LineReader reader;
        private InputLine inputLine;
        private InputVariable currentVar;
        private int currentVarLineNum;

        //---------------------------------------------------------------------

        // Local class for recording a specific line number for a line reader.
        private class FixedLineReader
            : LineReader
        {
            private LineReader reader;
            private int lineNumber;

            public override string SourceName
            {
                get {
                    return reader.SourceName;
                }
            }

            public override int LineNumber
            {
                get {
                    return lineNumber;
                }
            }

            public FixedLineReader(LineReader reader,
                                   int        lineNumber)
            {
                this.reader = reader;
                this.lineNumber = lineNumber;
            }

            protected override string GetNextLine()
            {
                throw new System.InvalidOperationException();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The current input line.
        /// </summary>
        protected string CurrentLine
        {
            get {
                Debug.Assert( inputLine != null );
                return inputLine.ToString();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The line number of the current line.
        /// </summary>
        protected int LineNumber
        {
            get {
                Debug.Assert( inputLine != null );
                return inputLine.Number;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The variable name at the start of the current line.
        /// </summary>
        protected string CurrentName
        {
            get {
                Debug.Assert( inputLine != null );
                return inputLine.VariableName;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// At end of input?
        /// </summary>
        protected bool AtEndOfInput
        {
            get {
                return LineNumber == LineReader.EndOfInput;
            }
        }

        //---------------------------------------------------------------------

        private void SetCurrentVar(InputVariable var)
        {
            currentVar = var;
            if (inputLine != null)
                currentVarLineNum = inputLine.Number;
            else
                currentVarLineNum = 0;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of TextParser.
        /// </summary>
        public TextParser()
        {
            reader = null;
            inputLine = null;
            SetCurrentVar(null);
        }

        //---------------------------------------------------------------------

        public T Parse(LineReader reader)
        {
            Require.ArgumentNotNull(reader);
            this.reader = reader;
            this.inputLine = new InputLine(reader);
            SetCurrentVar(null);

            try {
                return Parse();
            }
            catch (InputValueException valueExc) {
                if (currentVar == null) {
                    throw new LineReaderException(reader, valueExc);
                }
                else {
                    string message = string.Format("Error with the input value for {0}",
                                                   currentVar.Name);
                    InputVariableException varExc = new InputVariableException(currentVar,
                                                                               message,
                                                                               valueExc);
                    throw new LineReaderException(new FixedLineReader(reader, currentVarLineNum),
                                                  varExc);
                }
            }
            catch (InputVariableException varExc) {
                if (varExc.Variable == currentVar)
                    throw new LineReaderException(new FixedLineReader(reader, currentVarLineNum),
                                                  varExc);
                else
                    throw new LineReaderException(reader, varExc);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Parses text input and returns an instance of T.
        /// </summary>
        /// <remarks>
        /// Subclasses should implement this method using the protected members
        /// of this base class to read the text input.
        /// </remarks>
        protected abstract T Parse();

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the next line from the text input.
        /// </summary>
        /// <returns>
        /// true if there are no more lines in the input.
        /// </returns>
        protected bool GetNextLine()
        {
            Debug.Assert( inputLine != null );
            return inputLine.GetNext();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Reads the name and value of an input variable from the current
        /// line.
        /// </summary>
        /// <exception cref="LineReaderException">
        /// </exception>
        /// <remarks>
        /// After a call to this method, the property CurrentLine will refer
        /// to the next data line after the line with the variable that was
        /// just read.  The caller can perform additional error-checking on
        /// the variable's value after this method returns.  If the caller
        /// detects an error, it should throw an InputValueException or
        /// an InputVariableException.
        /// </remarks>
        protected void ReadVar(InputVariable var)
        {
            Debug.Assert( inputLine != null );
            inputLine.ReadVar(var);
            SetCurrentVar(var);
            GetNextLine();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Reads the name and value of an optional input variable from the
        /// current line.
        /// </summary>
        /// <returns>
        /// true if the name and value were read; false if the name on the
        /// current line does not match the variable's name.
        /// </returns>
        /// <exception cref="LineReaderException">
        /// </exception>
        /// <remarks>
        /// If this method return true, the property CurrentLine will refer
        /// to the next data line after the line with the variable that was
        /// just read.  The caller can perform additional error-checking on
        /// the variable's value after this method returns.  If the caller
        /// detects an error, it should throw an InputValueException or
        /// an InputVariableException.
        /// </remarks>
        protected bool ReadOptionalVar(InputVariable var)
        {
            Debug.Assert( inputLine != null );
            if (inputLine.ReadOptionalVar(var)) {
                SetCurrentVar(var);
                GetNextLine();
                return true;
            }
            else
                return false;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Reads a name from the current input line.
        /// </summary>
        /// <remarks>
        /// The line is expected to have only the given name.
        /// </remarks>
        /// <exception cref="LineReaderException">
        /// Thrown when the line is empty (has no name; LineReader
        /// is configured to return blank lines!).  Or when name on the line
        /// does not match the specified name.  Or when there's additional text
        /// after the name.
        /// </exception>
        protected void ReadName(string name)
        {
            Debug.Assert( inputLine != null );
            inputLine.MatchName(name);
            GetNextLine();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Reads an optional name from the current input line.
        /// </summary>
        /// <returns>
        /// true if the name on the current line matches the specified name;
        /// false if it does not match.
        /// </returns>
        /// <exception cref="LineReaderException">
        /// When the name on the current line matches the specified name, but
        /// there's additional text after the name.
        /// </exception>
        protected bool ReadOptionalName(string name)
        {
            Debug.Assert( inputLine != null );
            if (inputLine.MatchOptionalName(name)) {
                GetNextLine();
                return true;
            }
            else
                return false;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Reads the value of an input variable from the current line.
        /// </summary>
        /// <exception cref="InputValueException">
        /// </exception>
        /// <remarks>
        /// This method is used for reading column values from a table.  The
        /// current line is a row in the table.
        /// </remarks>
        protected void ReadValue(InputVariable var,
                                 StringReader  currentLine)
        {
            Require.ArgumentNotNull(var);
            Require.ArgumentNotNull(currentLine);
            var.ReadValue(currentLine);
            SetCurrentVar(var);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Checks to see if there are extra data after the last expected data
        /// of the text input.
        /// </summary>
        /// <exception cref="LineReaderException">
        /// If a non-blank line is found before the end of the input.
        /// </exception>
        protected void CheckNoDataAfter(string lastData)
        {
            while (! AtEndOfInput) {
                string currentLine = CurrentLine.TrimStart(null);
                if (currentLine.Length == 0)
                    GetNextLine();
                else
                    throw inputLine.ExtraTextException("Found extra data after " + lastData,
                                                       currentLine);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Checks to see if there are extra data after the last expected
        /// column data on the current line.
        /// </summary>
        /// <exception cref="LineReaderException">
        /// If a non-blank line is found before the end of the input.
        /// </exception>
        protected void CheckNoDataAfter(string       lastData,
                                        StringReader currentLine)
        {
            TextReader.SkipWhitespace(currentLine);
            string extraText = currentLine.ReadToEnd();
            if (extraText.Length > 0) {
                throw inputLine.ExtraTextException("Found extra data after " + lastData,
                                                   extraText);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new LineReaderException for the current line.
        /// </summary>
        /// <param name="message">
        /// The message explaining the exception.
        /// </param>
        protected LineReaderException NewParseException(string message)
        {
            return new LineReaderException(reader, message);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new LineReaderException for the current line.
        /// </summary>
        /// <param name="message">
        /// The message explaining the exception; it uses the placeholder
        /// notation "{n}" used by the System.String.Format method.
        /// </param>
        /// <param name="mesgArgs">
        /// The objects whose values as strings will be inserted into the
        /// message.
        /// </param>
        protected LineReaderException NewParseException(string          message,
                                                        params object[] mesgArgs)
        {
            return new LineReaderException(reader, message, mesgArgs);
        }
    }
}
