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
