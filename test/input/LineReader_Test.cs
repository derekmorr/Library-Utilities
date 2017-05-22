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
    //  Simple null wrapper around base class for testing purposes
    internal class NullLineReader
        : LineReader
    {
        public override string SourceName
        {
            get {
                return null;
            }
        }

        public NullLineReader()
            : base()
        {
        }

        protected override string GetNextLine()
        {
            return null;
        }
    }

    //-------------------------------------------------------------------------

    [TestFixture]
    public class LineReader_Test
    {
        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void CommentLineMarker_Empty()
        {
            try {
                LineReader reader = new NullLineReader();
                reader.CommentLineMarker = "";
            }
            catch (System.ApplicationException e) {
                Data.Output.WriteLine(e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void CommentLineMarker_1stCharWhitespace()
        {
            try {
                LineReader reader = new NullLineReader();
                reader.CommentLineMarker = " #";
            }
            catch (System.ApplicationException e) {
                Data.Output.WriteLine(e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void EOLCommentMarker_Empty()
        {
            try {
                LineReader reader = new NullLineReader();
                reader.EndCommentMarker = "";
            }
            catch (System.ApplicationException e) {
                Data.Output.WriteLine(e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void EOLCommentMarker_1stCharWhitespace()
        {
            try {
                LineReader reader = new NullLineReader();
                reader.EndCommentMarker = " //";
            }
            catch (System.ApplicationException e) {
                Data.Output.WriteLine(e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ReadLineAfterClose()
        {
            LineReader reader = new NullLineReader();
            reader.Close();
            string line = reader.ReadLine();
        }
    }
}
