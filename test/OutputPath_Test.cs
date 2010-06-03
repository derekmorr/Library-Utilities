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
using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class OutputPath_Test
    {
        [Test]
        public void Check_TwoNames()
        {
            IDictionary<string, bool> vars = new Dictionary<string, bool>();
            vars["species"] = true;
            vars["timestep"] = true;
            OutputPath.CheckTemplateVars("./output/{species}-{timestep}.gis", vars);
        }

        //--------------------------------------------------------------------

        private void TryCheckVars(string                    template,
                                  IDictionary<string, bool> variables)
        {
            try {
                OutputPath.CheckTemplateVars(template, variables);
            }
            catch (InputValueException exc) {
                Data.Output.WriteLine(exc.Message);
                Data.Output.WriteLine();
                throw;
            }
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Check_OneMissing()
        {
            IDictionary<string, bool> vars = new Dictionary<string, bool>();
            vars["species"] = true;
            vars["timestep"] = true;
            TryCheckVars("./output/{species}.gis", vars);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Check_TwoMissing()
        {
            IDictionary<string, bool> vars = new Dictionary<string, bool>();
            vars["species"] = true;
            vars["timestep"] = true;
            TryCheckVars("./output/species.gis", vars);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Check_OneUnknown()
        {
            IDictionary<string, bool> vars = new Dictionary<string, bool>();
            vars["species"] = true;
            TryCheckVars("./output/{species}-{timestep}.gis", vars);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(InputValueException))]
        public void Check_TwoUnknown()
        {
            IDictionary<string, bool> vars = new Dictionary<string, bool>();
            TryCheckVars("./output/{species}-{timestep}.gis", vars);
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_TwoNames()
        {
            IDictionary<string, string> vars = new Dictionary<string, string>();
            vars["species"] = "maple";
            vars["timestep"] = "350";
            Assert.AreEqual("./output/maple-350.gis",
                            OutputPath.ReplaceTemplateVars("./output/{species}-{timestep}.gis", vars));
        }

        //--------------------------------------------------------------------

        [Test]
        public void Replace_RepeatedName()
        {
            IDictionary<string, string> vars = new Dictionary<string, string>();
            vars["species"] = "maple";
            vars["timestep"] = "350";
            Assert.AreEqual("./maple/maple-350.gis",
                            OutputPath.ReplaceTemplateVars("./{species}/{species}-{timestep}.gis", vars));
        }

        //--------------------------------------------------------------------

        private void TryReplaceVars(string                      template,
                                    IDictionary<string, string> variables,
                                    string                      unknownVar)
        {
            try {
                string result = OutputPath.ReplaceTemplateVars(template, variables);
            }
            catch (ApplicationException exc) {
                Data.Output.WriteLine(exc.Message);
                Data.Output.WriteLine();
                Assert.AreEqual(template, exc.Data["Template"]);
                Assert.AreEqual(unknownVar, exc.Data["Variable"]);
                throw;
            }
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void Replace_Unknown()
        {
            IDictionary<string, string> vars = new Dictionary<string, string>();
            vars["species"] = "maple";
            TryReplaceVars("./output/{species}-{timestep}.gis", vars, "timestep");
        }
    }
}
