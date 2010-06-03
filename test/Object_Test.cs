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

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class Object_Test
    {
        private class TestClassA
        {
        }
        private TestClassA objectA1;
        private TestClassA objectA2;
        private class TestClassB
        {
        }
        private TestClassB objectB;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            objectA1 = new TestClassA();
            objectA2 = new TestClassA();
            objectB = new TestClassB();
        }

        //---------------------------------------------------------------------

        [Test]
        public void Compare_2Nulls()
        {
            Assert.AreEqual(Object.CompareResult.ReferToSame,
                            Object.Compare(null, null));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Compare_LeftOperandNull()
        {
            Assert.AreEqual(Object.CompareResult.OneIsNull,
                            Object.Compare(objectA1, null));
            Assert.AreEqual(Object.CompareResult.OneIsNull,
                            Object.Compare(objectB, null));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Compare_RightOperandNull()
        {
            Assert.AreEqual(Object.CompareResult.OneIsNull,
                            Object.Compare(null, objectA1));
            Assert.AreEqual(Object.CompareResult.OneIsNull,
                            Object.Compare(null, objectB));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Compare_SameObject()
        {
            Assert.AreEqual(Object.CompareResult.ReferToSame,
                            Object.Compare(objectA1, objectA1));
            Assert.AreEqual(Object.CompareResult.ReferToSame,
                            Object.Compare(objectA2, objectA2));
            Assert.AreEqual(Object.CompareResult.ReferToSame,
                            Object.Compare(objectB, objectB));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Compare_DifferentInstances()
        {
            Assert.AreEqual(Object.CompareResult.DifferentInstances,
                            Object.Compare(objectA1, objectA2));
            Assert.AreEqual(Object.CompareResult.DifferentInstances,
                            Object.Compare(objectA1, objectB));
            Assert.AreEqual(Object.CompareResult.DifferentInstances,
                            Object.Compare(objectB, objectA2));
        }
    }
}
