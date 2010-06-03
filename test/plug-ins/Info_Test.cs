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

using Edu.Wisc.Forest.Flel.Util.PlugIns;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Edu.Wisc.Forest.Flel.Test.Util.PlugIns
{
    //  A plug-in interface & a couple classes that implement it

    public interface PlugInInterface1
        : IPlugIn
    {
        int SimpleMethod();
    }

    public class PlugIn1A
        : PlugInInterface1
    {
        public string Name { get { return "PlugIn1A"; } }

        public int SimpleMethod() { return 50; }
    }

    public class PlugIn1Blue
        : PlugInInterface1
    {
        public string Name { get { return "PlugIn1-Blue"; } }

        public int SimpleMethod() { return -987; }
    }

    //-------------------------------------------------------------------------

    //  Another plug-in interface & a class that implements it

    public interface PlugInInterface2
        : IPlugIn
    {
        bool IsActive { get; }
    }

    public class PlugIn2A
        : PlugInInterface2
    {
        public string Name { get { return "PlugIn2A"; } }

        public bool IsActive { get { return false; } }
    }

    //-------------------------------------------------------------------------

    //  A 3rd plug-in interface that's not implemented

    public interface PlugInInterface3
        : IPlugIn
    {
        void ChewGumAndWalk();
    }

    //-------------------------------------------------------------------------

    [TestFixture]
    public class Info_Test
    {
        [Test]
        public void GetPlugIns()
        {
            System.Type[] plugInInterfaces = new System.Type[] {
                typeof(PlugInInterface1),
                typeof(PlugInInterface2),
                typeof(PlugInInterface3)
            };
            IInfo[] plugIns = Info.GetPlugIns(Assembly.GetExecutingAssembly(),
                                              plugInInterfaces);

            List<IInfo> expectedPlugIns = new List<IInfo>();
            expectedPlugIns.Add(new Info("PlugIn1A",
                                         typeof(PlugInInterface1),
                                         typeof(PlugIn1A).AssemblyQualifiedName));
            expectedPlugIns.Add(new Info("PlugIn1-Blue",
                                         typeof(PlugInInterface1),
                                         typeof(PlugIn1Blue).AssemblyQualifiedName));
            expectedPlugIns.Add(new Info("PlugIn2A",
                                         typeof(PlugInInterface2),
                                         typeof(PlugIn2A).AssemblyQualifiedName));

            Assert.AreEqual(expectedPlugIns.Count, plugIns.Length);
            foreach (IInfo plugIn in plugIns)
                AssertPlugInIsExpected(plugIn, expectedPlugIns);
        }

        //---------------------------------------------------------------------

        private void AssertPlugInIsExpected(IInfo       plugIn,
                                            List<IInfo> expectedPlugIns)
        {
            bool found = false;
            foreach (IInfo expectedPlugIn in expectedPlugIns) {
                if (expectedPlugIn.Name == plugIn.Name) {
                    Assert.AreEqual(expectedPlugIn.InterfaceType,
                                    plugIn.InterfaceType);
                    Assert.AreEqual(expectedPlugIn.ImplementationName,
                                    plugIn.ImplementationName);
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }
    }
}
