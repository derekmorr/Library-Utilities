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

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class Percentage_Test
    {
        [Test]
        public void DefaultCtor()
        {
            Percentage percentage = new Percentage();
            Assert.AreEqual(0.0, percentage.Value);
            Assert.AreEqual(0.0, (double) percentage);
            Assert.AreEqual("0%", percentage.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void DoubleCtor()
        {
            Percentage percentage = new Percentage(0.567);
            Assert.AreEqual(0.567, percentage.Value);
            Assert.AreEqual(0.567, (double) percentage);
            Assert.AreEqual("56.7%", percentage.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void Parse_50Percent()
        {
            Percentage percentage = Percentage.Parse("50%");
            Check50Percent(percentage);
        }

        //---------------------------------------------------------------------

        private void Check50Percent(Percentage percentage)
        {
            Assert.AreEqual(0.50, percentage.Value);
            Assert.AreEqual("50%", percentage.ToString());
            Assert.AreEqual("50%", string.Format("{0}", percentage));
            Assert.AreEqual("50%", string.Format("{0:%}", percentage));
            Assert.AreEqual("50.0%", string.Format("{0:#.0%}", percentage));
            Assert.AreEqual(".500", string.Format("{0:#.##0}", percentage));
            Assert.AreEqual(".5", string.Format("{0:#.###}", percentage));
            Assert.AreEqual("0.50", string.Format("{0:0.#0}", percentage));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Parse_50Percent_Whitespace()
        {
            Percentage percentage = Percentage.Parse("\t 50% \n");
            Check50Percent(percentage);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.OverflowException))]
        public void CtorArg_TooBig()
        {
            Percentage percentage = new Percentage(double.MaxValue);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.OverflowException))]
        public void CtorArg_TooSmall()
        {
            Percentage percentage = new Percentage(double.MinValue);
        }

        //---------------------------------------------------------------------

        private void TryParse(string str)
        {
            try {
                Percentage percentage = Percentage.Parse(str);
            }
            catch (System.Exception exc) {
                Data.Output.WriteLine(exc.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Parse_Null()
        {
            TryParse(null);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.FormatException))]
        public void Parse_Empty()
        {
            TryParse("");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.FormatException))]
        public void Parse_Whitespace()
        {
            TryParse(" \t ");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.FormatException))]
        public void Parse_JustValue()
        {
            TryParse("50");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.FormatException))]
        public void Parse_JustPercentSign()
        {
            TryParse("%");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.FormatException))]
        public void Parse_BadValue()
        {
            TryParse("-5e%");
        }
    }
}
