using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class Directory_Test
    {
        [Test]
        public void DataDir()
        {
            Directory.EnsureExists(Data.Directory);
        }

        //--------------------------------------------------------------------

        [Test]
        public void CurrentDir()
        {
            Directory.EnsureExists(".");
        }

        //--------------------------------------------------------------------

        private void TryEnsureExists(string path)
        {
            try {
                Directory.EnsureExists(path);
            }
            catch (System.Exception exc) {
                Data.Output.WriteLine(exc.Message);
                throw;
            }
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(MultiLineException))]
        public void Null()
        {
            TryEnsureExists(null);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(MultiLineException))]
        public void InvalidChars()
        {
            TryEnsureExists("foo<bar");
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void FileExists()
        {
            TryEnsureExists(System.IO.Path.Combine(Data.Directory,
                                                   "DirectoryTest_FileExists.txt"));
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(MultiLineException))]
        public void ParentIsFile()
        {
            string filename = System.IO.Path.Combine(Data.Directory,
                                                    "DirectoryTest_FileExists.txt");
            TryEnsureExists(System.IO.Path.Combine(filename, "dir-whose-parent-is-file"));
        }
    }
}
