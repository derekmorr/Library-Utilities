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

using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Edu.Wisc.Forest.Flel.Util
{
    public class MultiLineText
        : IEnumerable<string>
    {
        private List<string> lines;

        //---------------------------------------------------------------------

        public int Count
        {
            get {
                return lines.Count;
            }
        }

        //---------------------------------------------------------------------

        public string this[int index]
        {
            get {
                return lines[index];
            }
        }

        //---------------------------------------------------------------------

        public MultiLineText()
        {
            lines = new List<string>();
        }

        //---------------------------------------------------------------------

        public MultiLineText(string line)
        {
            lines = new List<string>(1);
            lines.Add(line);
        }

        //---------------------------------------------------------------------

        public static implicit operator MultiLineText(string line)
        {
            return new MultiLineText(line);
        }

        //---------------------------------------------------------------------

        public MultiLineText(string[] lines)
        {
            this.lines = new List<string>(lines.Length);
            foreach (string line in lines)
                this.lines.Add(line);
        }

        //---------------------------------------------------------------------

        public static implicit operator MultiLineText(string[] lines)
        {
            return new MultiLineText(lines);
        }

        //---------------------------------------------------------------------

        public MultiLineText(IEnumerable<string> lines)
        {
            this.lines = new List<string>(lines);
        }

        //---------------------------------------------------------------------

        public static implicit operator MultiLineText(List<string> lines)
        {
            return new MultiLineText(lines);
        }

        //---------------------------------------------------------------------

        public MultiLineText Clone()
        {
            MultiLineText clone = new MultiLineText(lines);
            return clone;
        }

        //---------------------------------------------------------------------

        public IEnumerator<string> GetEnumerator()
        {
            foreach (string line in lines)
                yield return line;
        }

        //---------------------------------------------------------------------

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //---------------------------------------------------------------------

        public MultiLineText Add(string line)
        {
            lines.Add(line);
            return this;
        }

        //---------------------------------------------------------------------

        public static MultiLineText operator+(MultiLineText x,
                                              MultiLineText y)
        {
            MultiLineText result = x.Clone();
            foreach (string line in y)
                result.Add(line);
            return result;
        }

        //---------------------------------------------------------------------

        public override string ToString()
        {
            if (lines.Count == 0)
                return "";
            if (lines.Count == 1)
                return lines[0];

            int length = 0;
            foreach (string line in lines)
                length += line.Length;
            length += System.Environment.NewLine.Length * (lines.Count - 1);
            StringBuilder strBuilder = new StringBuilder(length);
            foreach (string line in lines.GetRange(0, lines.Count - 1))
                strBuilder.AppendLine(line);
            strBuilder.Append(lines[lines.Count - 1]);
            return strBuilder.ToString();
        }
    }
}
