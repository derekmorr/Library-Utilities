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
using System;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class InputVar_Percentage_Test
    {
        InputVar<Percentage> percentVar;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            //  Need to reference the class so its static ctor is called before
            //  any tests run; otherwise, they'll fail because a parse or read
            //  method hasn't been registered with InputValues class.
            Percentage p = new Percentage();

            percentVar = new InputVar<Percentage>("Percentage Var");
        }

        //---------------------------------------------------------------------

        private void PrintInputVarException(string input)
        {
            try {
                StringReader reader = new StringReader(input);
                percentVar.ReadValue(reader);
            }
            catch (InputVariableException exc) {
                Data.Output.WriteLine(exc.Message);
                throw exc;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void EmptyString()
        {
            PrintInputVarException("");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void Whitespace()
        {
            PrintInputVarException(" \t \r ");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void NoPercentChar()
        {
            PrintInputVarException("50.0");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void DecimalPt()
        {
            PrintInputVarException(".%");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void JustExponent()
        {
            PrintInputVarException("e4%");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void NoExponentDigits()
        {
            PrintInputVarException("-1.23e+%");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void TooBig()
        {
            PrintInputVarException("9e+999%");
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void DoubleMax()
        {
            PrintInputVarException(string.Format("{0}%", double.MaxValue));
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputVariableException))]
        public void DoubleMin()
        {
            PrintInputVarException(string.Format("{0}%", double.MinValue));
        }

        //---------------------------------------------------------------------

        private void CheckReadResults(string input,
                                      double expectedValue,
                                      string expectedString,
                                      int    expectedIndex)
        {
            StringReader reader = new StringReader(input);
            percentVar.ReadValue(reader);
            Assert.AreEqual(expectedValue, percentVar.Value.Actual.Value);
            Assert.AreEqual(expectedString, percentVar.Value.String);
            Assert.AreEqual(expectedIndex, percentVar.Index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void JustDigits()
        {
            CheckReadResults("1234%", 12.34, "1234%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DigitsDecimalPt()
        {
            CheckReadResults("4.%", 0.04, "4.%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DigitsDecimalPtExponent()
        {
            CheckReadResults("4.e3%", 4e1, "4.e3%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DecimalPtDigits()
        {
            CheckReadResults(".78%", .78/100, ".78%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void PlusDigits()
        {
            CheckReadResults("+1,234%", 12.34, "+1,234%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void MinusDigits()
        {
            CheckReadResults("-1234%", -12.34, "-1234%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void LeadingWhiteSpace()
        {
            CheckReadResults(" \t -1234%", -12.34, "-1234%", 3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void TrailingWhiteSpace()
        {
            CheckReadResults("-1234% \n ", -12.34, "-1234%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void NumWithWhiteSpace()
        {
            CheckReadResults(" \t -1,234% \n ", -12.34, "-1,234%", 3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Thousands()
        {
            CheckReadResults("12,345,678%", 123456.78, "12,345,678%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ThousandsDecimalPtExponent()
        {
            CheckReadResults("12,345.67e+02%", 12345.67, "12,345.67e+02%", 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void PercentageMax()
        {
            Percentage percentage = new Percentage(Percentage.MaxValueAsDouble);
            Assert.AreEqual(string.Format("{0}%", double.MaxValue),
                            percentage.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void PercentageMin()
        {
            Percentage percentage = new Percentage(Percentage.MinValueAsDouble);
            Assert.AreEqual(string.Format("{0}%", double.MinValue),
                            percentage.ToString());
        }

        //---------------------------------------------------------------------

        [Test]
        public void StringOfValues()
        {
            double[] values = new double[] { -4, .00789, 0, 5.55e3 };
            string[] valsAsStrs = Array.ConvertAll(values,
                                                   new Converter<double, string>(Convert.ToString));
            string valuesAsStr = string.Join("% ", valsAsStrs) + "%";

            StringReader reader = new StringReader(valuesAsStr);
            foreach (double d in values) {
                percentVar.ReadValue(reader);
                Assert.AreEqual(d/100, (double) percentVar.Value.Actual);
            }
        }
    }
}
