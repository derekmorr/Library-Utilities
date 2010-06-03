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
    /// A LineReader whose source is a collection of strings.
    /// </summary>
    public class TextLineReader
        : LineReader
    {
        private string sourceName;
        private MultiLineText text;

        //---------------------------------------------------------------------

        public override string SourceName
        {
            get {
                return sourceName;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance from text with multiple lines.
        /// </summary>
        public TextLineReader(MultiLineText text)
        {
            this.sourceName = null;
            this.text = text;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance from text with multiple lines.
        /// </summary>
        public TextLineReader(string        sourceName,
                              MultiLineText text)
        {
            this.sourceName = sourceName;
            this.text = text;
        }

        //---------------------------------------------------------------------

        protected override string GetNextLine()
        {
            if (LineNumber == LineReader.EndOfInput)
                return null;
            if (text == null)
                return null;
            //  LineNumber is the number of the previous line, and it also
            //  happens to be the index of the next line to return.
            if (LineNumber < text.Count)
                return text[LineNumber];
            return null;
        }
    }
}
