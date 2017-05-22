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
using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class Macros_Test
    {
        private string[] noNames;

        //--------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            noNames = new string[0];
        }

        //--------------------------------------------------------------------

        private void AssertSameNames(string[]      expected,
                                     IList<string> actual)
        {
            Assert.AreEqual(expected.Length, actual.Count);
            foreach (string name in expected)
                Assert.IsTrue(actual.IndexOf(name) != -1);
        }

        //--------------------------------------------------------------------

        [Test]
        public void EmptyStr()
        {
            IList<string> names = Macros.GetNames("");
            AssertSameNames(noNames, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_NoRightBrace()
        {
            IList<string> names = Macros.GetNames("Roses are {red");
            AssertSameNames(noNames, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_EmptyName()
        {
            IList<string> names = Macros.GetNames("Roses are {} red");
            string[] expected = new string[]{ "" };
            AssertSameNames(expected, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_SpecialMacro()
        {
            IList<string> names = Macros.GetNames("The '{LEFT-BRACE}' key is not working.");
            AssertSameNames(noNames, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_TwoNames()
        {
            IList<string> names = Macros.GetNames("./output/{species}_{timestep}.gis");
            string[] expected = new string[]{ "species", "timestep" };
            AssertSameNames(expected, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_RepeatedName()
        {
            IList<string> names = Macros.GetNames("./{foo}/{foo}-{bar}.dat");
            string[] expected = new string[]{ "bar", "foo" };
            AssertSameNames(expected, names);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Get_RepeatedAndSpecial()
        {
            IList<string> names = Macros.GetNames("./{foo}/{foo}-{bar}_{LEFT-BRACE}test}.dat");
            string[] expected = new string[]{ "bar", "foo" };
            AssertSameNames(expected, names);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Replace_NullStr()
        {
            string result = Macros.Replace(null, new Dictionary<string, string>());
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Replace_NullMacros()
        {
            string result = Macros.Replace("", null);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_TwoNames()
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            macros["species"] = "maple";
            macros["timestep"] = "250";
            string result = Macros.Replace("./output/{species}_{timestep}.gis", macros);
            Assert.AreEqual("./output/maple_250.gis", result);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_LeftBraceAtEnd()
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            macros["species"] = "maple";
            macros["timestep"] = "250";
            string result = Macros.Replace("{species}{timestep}.gis{", macros);
            Assert.AreEqual("maple250.gis{", result);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_NoRightBrace()
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            macros["species"] = "maple";
            macros["timestep"] = "250";
            string result = Macros.Replace("{species}-{timestep.gis", macros);
            Assert.AreEqual("maple-{timestep.gis", result);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_Special()
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            macros["species"] = "maple";
            macros["timestep"] = "250";
            string result = Macros.Replace("{species}-{LEFT-BRACE}test}.txt", macros);
            Assert.AreEqual("maple-{test}.txt", result);
        }

        //--------------------------------------------------------------------

        private void TryReplace(string                      str,
                                IDictionary<string, string> macros,
                                string                      badName)
        {
            try {
                string result = Macros.Replace(str, macros);
            }
            catch (System.ApplicationException exc) {
                Assert.AreEqual(badName, exc.Data["Name"]);
                Assert.AreEqual("{" + badName + "}", exc.Data["DelimitedName"]);
                throw;
            }
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void Replace_Unknown()
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            TryReplace("A string with an {unknown} macro", macros, "unknown");
        }
    }
}
