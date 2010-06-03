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
using System;
using System.Diagnostics;
using System.IO;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class InputVar_Test
    {
        //  Inner classes for testing GetReadMethod
        public class UnregisteredClass
        {
        }

        public class RegisteredClass
        {
            public readonly string Str;

            private RegisteredClass(string s)
            {
                Str = s;
            }

            public static RegisteredClass Parse(string s)
            {
                return new RegisteredClass(s);
            }
        }

        private TraceListener[] listeners;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            listeners = Edu.Wisc.Forest.Flel.Util.Diagnostics.TraceListener.Copy(Debug.Listeners);
            Debug.Listeners.Clear();
            Debug.Listeners.Add(new Edu.Wisc.Forest.Flel.Util.Diagnostics.TraceListener());
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ReadMethodNull()
        {
            InputVar<int> var = new InputVar<int>("var", (ReadMethod<int>) null);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void GetReadMethod_UnregisteredType()
        {
            try {
                ReadMethod<UnregisteredClass> readMethod =
                    InputValues.GetReadMethod<UnregisteredClass>();
            }
            catch (Exception exc) {
                Data.Output.WriteLine(exc.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Debug.Listeners.Clear();
            Debug.Listeners.AddRange(listeners);
        }
    }
}
