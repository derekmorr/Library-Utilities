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
using System.Reflection;

namespace Edu.Wisc.Forest.Flel.Test.Util.PlugIns
{
    //  An interface for calls to Loader.Load method.
    public interface MyPlugInInterface
    {
        int SimpleMethod();
    }

    //-------------------------------------------------------------------------

    //  A plug-in class that does NOT implement MyPlugInInterface
    public class NullPlugIn
    {
    }

    //-------------------------------------------------------------------------

    internal class Magical
    {
        internal const int Number = 42;
    }

    //  A plug-in class that implements MyPlugInInterface
    public class MyPlugIn
        : MyPlugInInterface
    {
        public int SimpleMethod()
        {
            return Magical.Number;
        }
    }

    //-------------------------------------------------------------------------

    [TestFixture]
    public class Loader_Test
    {
        private string myAssemblyName;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            myAssemblyName = Assembly.GetExecutingAssembly().FullName;
        }

        //---------------------------------------------------------------------

        private void TryLoad<T>(IInfo plugInInfo)
        {
            try {
                Data.Output.WriteLine("Loading the {0} plug-in \"{1}\"...",
                                      plugInInfo.InterfaceType.Name,
                                      plugInInfo.Name);
                T plugIn = Loader.Load<T>(plugInInfo);
                Assert.IsNotNull(plugIn);
            }
            catch (System.Exception e) {
                Data.Output.WriteLine(e.Message);
                Data.Output.WriteLine();
                throw;
            }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_SystemType_Null()
        {
            IInfo info = new Info("null system type",
                                  typeof(MyPlugInInterface),
                                  null);
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_SystemType_Empty()
        {
            IInfo info = new Info("empty system type",
                                  typeof(MyPlugInInterface),
                                  "");
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_SystemType_BadChars()
        {
            IInfo info = new Info("system type with bad chars",
                                  typeof(MyPlugInInterface),
                                  "..,");
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_NonexistantAssembly()
        {
            IInfo info = new Info("nonexistant assembly",
                                  typeof(MyPlugInInterface),
                                  "UndefinedClass,NonexistantAssembly");
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_UndefinedClass()
        {
            IInfo info = new Info("undefined class",
                                  typeof(MyPlugInInterface),
                                  "UndefinedClass,"  + myAssemblyName);
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(Flel.Util.PlugIns.Exception))]
        public void Load_InterfaceNotSupported()
        {
            IInfo info = new Info("interface not supported",
                                  typeof(MyPlugInInterface),
                                  typeof(NullPlugIn).AssemblyQualifiedName);
            TryLoad<MyPlugInInterface>(info);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Load_GoodPlugIn()
        {
            IInfo info = new Info("good plug-in",
                                  typeof(MyPlugInInterface),
                                  typeof(MyPlugIn).AssemblyQualifiedName);
            MyPlugInInterface plugIn = Loader.Load<MyPlugInInterface>(info);
            Assert.IsNotNull(plugIn);
            Assert.AreEqual(Magical.Number, plugIn.SimpleMethod());
        }
    }
}
