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
