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
    public class Indexes_Test
    {
        private void CheckIndexes(Indexes      indexes,
                                  params int[] expectedIndexes)
        {
            if (expectedIndexes == null)
                expectedIndexes = new int[0];

            int i = 0;
            foreach (int index in indexes) {
                Assert.AreEqual(expectedIndexes[i], index);
                i++;
            }
            Assert.AreEqual(expectedIndexes.Length, i);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StartLessThanEnd()
        {
            Indexes indexes = Indexes.Between(1, 5);
            CheckIndexes(indexes, 1, 2, 3, 4, 5);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StartEqualEnd()
        {
            Indexes indexes = Indexes.Between(567, 567);
            CheckIndexes(indexes, 567);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StartMoreThanEnd()
        {
            Indexes indexes = Indexes.Between(23, 17);
            CheckIndexes(indexes, 23, 22, 21, 20, 19, 18, 17);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntArray_Empty()
        {
            Indexes indexes = Indexes.Of(new int[0]);
            CheckIndexes(indexes);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntArray_1Item()
        {
            Indexes indexes = Indexes.Of(new int[]{ -77 });
            CheckIndexes(indexes, 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntArray_ManyItems()
        {
            Indexes indexes = Indexes.Of(new int[]{ -1, -2, -3, -4, -55 });
            CheckIndexes(indexes, 0, 1, 2, 3, 4);
        }

        //---------------------------------------------------------------------

        [Test]
        public void IntArray_Reverse()
        {
            Indexes indexes = Indexes.Of(new int[]{ 10, 20, 30, 40, 50, 60 });
            CheckIndexes(indexes.Reverse, 5, 4, 3, 2, 1, 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrArray_Empty()
        {
            Indexes indexes = Indexes.Of(new string[0]);
            CheckIndexes(indexes);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrArray_1Item()
        {
            Indexes indexes = Indexes.Of(new string[]{ "hello" });
            CheckIndexes(indexes, 0);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrArray_ManyItems()
        {
            Indexes indexes = Indexes.Of(new string[]{ "Alaska",
                                                       "California",
                                                       "New York",
                                                       "Vermont"  });
            CheckIndexes(indexes, 0, 1, 2, 3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrList_Empty()
        {
            List<string> list = new List<string>();
            Indexes indexes = Indexes.Of(list);
            CheckIndexes(indexes);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrList_Empty_Reverse()
        {
            List<string> list = new List<string>();
            Indexes indexes = Indexes.Of(list).Reverse;
            CheckIndexes(indexes);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrList_ManyItems()
        {
            string[] strs = new string[]{ "Alaska",
                                          "California",
                                          "New York",
                                          "Vermont"  };
            List<string> list = new List<string>(strs);
            Indexes indexes = Indexes.Of(list);
            CheckIndexes(indexes, 0, 1, 2, 3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void StrList_Reverse()
        {
            string[] strs = new string[]{ "Alaska",
                                          "California",
                                          "New York",
                                          "Vermont"  };
            List<string> list = new List<string>(strs);
            Indexes indexes = Indexes.Of(list).Reverse;
            CheckIndexes(indexes, 3, 2, 1, 0);
        }
    }
}
