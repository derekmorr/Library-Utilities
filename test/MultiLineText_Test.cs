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

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class MultiLineText_Test
    {
        private string[] Gettysburg;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            Gettysburg = new string[] { "Fourscore and seven years ago ",
                                        "our fathers brought forth on",
                                        "this continent a new nation,",
                                        " conceived in liberty and ",
                                        "dedicated to the proposition ",
                                        "that all men are created equal."};
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultCtor()
        {
            MultiLineText text = new MultiLineText();
            Assert.AreEqual(0, text.Count);
            Assert.AreEqual("", text.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void StringCtor()
        {
            string line = Gettysburg[0];
            MultiLineText text = new MultiLineText(line);
            Assert.AreEqual(1, text.Count);
            Assert.AreEqual(line, text.ToString());
            Assert.AreEqual(line, text[0]);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StringCtor_Implicit()
        {
            string line = Gettysburg[0];
            MultiLineText text = line;
            Assert.AreEqual(1, text.Count);
            Assert.AreEqual(line, text.ToString());
            Assert.AreEqual(line, text[0]);
        }

        //---------------------------------------------------------------------

        private string JoinLines(string[] lines)
        {
            string result = "";
            for (int i = 0; i < lines.Length - 1; i++)
                result += lines[i] + System.Environment.NewLine;
            if (lines.Length > 0)
                result += lines[lines.Length - 1];
            return result;
        }

        //---------------------------------------------------------------------

        private void AssertLinesEqual(string[]      lines,
                                      MultiLineText text)
        {
            Assert.AreEqual(lines.Length, text.Count);
            for (int i = 0; i < lines.Length; ++i)
                Assert.AreEqual(lines[i], text[i]);
            Assert.AreEqual(JoinLines(lines), text.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrArrayCtor()
        {
            string[] lines = Gettysburg;
            MultiLineText text = new MultiLineText(lines);
            AssertLinesEqual(lines, text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrArrayCtor_Implicit()
        {
            string[] lines = Gettysburg;
            MultiLineText text = lines;
            AssertLinesEqual(lines, text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IEnumeratorCtor()
        {
            List<string> lines = new List<string>();
            foreach (string line in Gettysburg)
                lines.Add(line);

            MultiLineText text = new MultiLineText(lines);
            AssertLinesEqual(lines.ToArray(), text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void CopyCtor()
        {
            string[] lines = Gettysburg;
            MultiLineText text1 = new MultiLineText(lines);
            MultiLineText text2 = new MultiLineText(text1);
            text1 = null;

            AssertLinesEqual(lines, text2);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ListStrCtor_Implicit()
        {
            List<string> lines = new List<string>();
            foreach (string line in Gettysburg)
                lines.Add(line);

            MultiLineText text = lines;
            AssertLinesEqual(lines.ToArray(), text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EnumeratorMethod()
        {
            string[] lines = Gettysburg;
            MultiLineText text = new MultiLineText(lines);
            Assert.AreEqual(lines.Length, text.Count);
            int i = 0;
            foreach (string line in text) {
                Assert.IsTrue(i < lines.Length);
                Assert.AreEqual(lines[i], line);
                i++;
            }
            Assert.AreEqual(lines.Length, i);
        }

        //---------------------------------------------------------------------

        [Test]
        public void AddMethod()
        {
            MultiLineText text = new MultiLineText();
            string[] lines = Gettysburg;
            foreach (string line in lines)
                text.Add(line);

            AssertLinesEqual(lines, text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void OperatorPlus_Str()
        {
            MultiLineText text = new MultiLineText();
            string[] lines = Gettysburg;
            foreach (string line in lines)
                text += line;

            AssertLinesEqual(lines, text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void OperatorPlus_StrArray()
        {
            MultiLineText text = new MultiLineText();
            string[] lines = Gettysburg;
            text += lines;

            AssertLinesEqual(lines, text);
        }

        //---------------------------------------------------------------------

        [Test]
        public void OperatorPlus_ListStr()
        {
            List<string> lines = new List<string>();
            foreach (string line in Gettysburg)
                lines.Add(line);

            MultiLineText text = new MultiLineText();
            text += lines;

            AssertLinesEqual(lines.ToArray(), text);
        }
    }
}
