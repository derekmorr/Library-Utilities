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
    public class Info_Test
    {
        [Test]
        public void WriteAssemblies_Null()
        {
            string[] prefixes = null;
            AssemblyInfo.WriteLoadedAssemblies(prefixes, Data.Output);
        }

        //---------------------------------------------------------------------

        [Test]
        public void WriteAssemblies_Array0()
        {
            string[] prefixes = new string[0];
            AssemblyInfo.WriteLoadedAssemblies(prefixes, Data.Output);
        }

        //---------------------------------------------------------------------

        [Test]
        public void WriteAssemblies_Array1()
        {
            string[] prefixes = new string[]{ "Edu.Wisc.Forest.Flel" };
            AssemblyInfo.WriteLoadedAssemblies(prefixes, Data.Output);
        }
    }
}
