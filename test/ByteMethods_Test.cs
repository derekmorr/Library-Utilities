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
    public class ByteMethods_Test
    {
        [Test]
        public void Byte()
        {
            IByteMethods<byte> methods =
                                        new Edu.Wisc.Forest.Flel.Util.ByteMethods.Byte();
            byte origValue = (byte) 123;
            byte[] bytes = methods.ToBytes(origValue);
            Assert.AreEqual(bytes.Length, sizeof(byte));
            Assert.AreEqual(origValue, bytes[0]);

            byte fromMthdResult = methods.FromBytes(bytes, 0);
            Assert.AreEqual(origValue, fromMthdResult);
        }

        //---------------------------------------------------------------------

        [Test]
        public void SByte()
        {
            IByteMethods<sbyte> methods =
                                        new Edu.Wisc.Forest.Flel.Util.ByteMethods.SByte();
            sbyte origValue = (sbyte) -123;
            byte[] bytes = methods.ToBytes(origValue);
            Assert.AreEqual(bytes.Length, sizeof(sbyte));

            sbyte fromMthdResult = methods.FromBytes(bytes, 0);
            Assert.AreEqual(origValue, fromMthdResult);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Short()
        {
            IByteMethods<short> methods =
                                        new Edu.Wisc.Forest.Flel.Util.ByteMethods.Short();
            short origValue = (short) -12345;
            byte[] bytes = methods.ToBytes(origValue);
            Assert.AreEqual(bytes.Length, sizeof(short));

            short fromMthdResult = methods.FromBytes(bytes, 0);
            Assert.AreEqual(origValue, fromMthdResult);
        }

        //---------------------------------------------------------------------

        [Test]
        public void UShort()
        {
            IByteMethods<ushort> methods =
                                        new Edu.Wisc.Forest.Flel.Util.ByteMethods.UShort();
            ushort origValue = (ushort) 12345;
            byte[] bytes = methods.ToBytes(origValue);
            Assert.AreEqual(bytes.Length, sizeof(ushort));

            ushort fromMthdResult = methods.FromBytes(bytes, 0);
            Assert.AreEqual(origValue, fromMthdResult);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Float()
        {
            IByteMethods<float> methods =
                                        new Edu.Wisc.Forest.Flel.Util.ByteMethods.Float();
            float origValue = (float) -9.876e5;
            byte[] bytes = methods.ToBytes(origValue);
            Assert.AreEqual(bytes.Length, sizeof(float));

            float fromMthdResult = methods.FromBytes(bytes, 0);
            Assert.AreEqual(origValue, fromMthdResult);
        }
    }
}
