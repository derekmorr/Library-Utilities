using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using Generic = System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    public class MyRandomNumGenerator
        : System.Random
    {
        public MyRandomNumGenerator()
            : base()
        {
        }

        public override int Next(int maxValue)
        {
            switch (maxValue) {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 3:
                    return 0;
                case 4:
                    return 3;
                case 5:
                    return 2;
                default:
                    Assert.Fail("Expected maxValue between 1 and 5");
                    return 0;
            }
        }
    }

    //-------------------------------------------------------------------------

    [TestFixture]
    public class List_Test
    {
        System.Random randomNumGen;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            randomNumGen = new MyRandomNumGenerator();
        }

        //--------------------------------------------------------------------

        [Test]
        public void NullList()
        {
            Generic.List<int> list = null;
            Assert.IsNull(List.Shuffle(list, randomNumGen));
        }

        //--------------------------------------------------------------------

        [Test]
        public void ListOfDoubles()
        {
            double[] values = new double[]{
                5.0, -4.0, 333.0, -0.2, 1
            };
            double[] expectedValues = new double[]{
                //  indexes = [0,1,2,3,4], so maxValue = 5 --> index at 2 returned
                333.0,
                //  indexes = [0,1,3,4], so maxValue = 4 --> index at 3 returned
                1,
                //  indexes = [0,1,3], so maxValue = 3 --> index at 0 returned
                5.0,
                //  indexes = [1,3], so maxValue = 2 --> index at 1 returned
                -0.2,
                //  indexes = [1], so maxValue = 1 --> index at 0 returned
                -4.0
            };

            Generic.IList<double> list = new Generic.List<double>(values);
            Generic.IList<double> shuffledList = List.Shuffle(list,
                                                              randomNumGen);
            Assert.AreEqual(expectedValues.Length, shuffledList.Count);
            for (int i = 0; i < expectedValues.Length; i++)
                Assert.AreEqual(expectedValues[i], shuffledList[i]);
        }

        //--------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void NullRandomNumGen()
        {
            Generic.IList<byte> list = new Generic.List<byte>();
            List.Shuffle(list, null);
        }
    }
}
