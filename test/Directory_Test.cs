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
