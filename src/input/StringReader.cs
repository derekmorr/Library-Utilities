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
    public class StringReader
        : System.IO.StringReader
    {
        private int index;

        //---------------------------------------------------------------------

        /// <summary>
        /// Index (position) of the next character that will be read.
        /// </summary>
        public int Index
        {
            get {
                return index;
            }
        }

        //---------------------------------------------------------------------

        public StringReader(string str)
            : base(str)
        {
            index = 0;
        }

        //---------------------------------------------------------------------

        public override int Read()
        {
            int result = base.Read();
            if (result != -1)
                index++;
            return result;
        }

        //---------------------------------------------------------------------

        public override int Read(char[] buffer,
                                 int    index,
                                 int    count)
        {
            int countRead = base.Read(buffer, index, count);
            this.index += countRead;
            return countRead;
        }
    }
}
