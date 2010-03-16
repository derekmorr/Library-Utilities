using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class Range_Test
    {
        [Test]
        public void AllFloats()
        {
            Range<float> allFloats = new Range<float>();
            CheckFloatRange(allFloats);
        }

        //--------------------------------------------------------------------

        [Test]
        public void AllFloats_EndpointsExcluded()
        {
            Range<float> range = new Range<float>();
            range.MinIncluded = false;
            range.MaxIncluded = false;
            CheckFloatRange(range);
        }

        //--------------------------------------------------------------------

        [Test]
        public void AllFloats_EndpointsExcluded_Operators()
        {
            Range<float> range = float.MaxValue > new Range<float>() > float.MinValue;
            CheckFloatRange(range);
        }

        //--------------------------------------------------------------------

        private void CheckFloatRange(Range<float> range)
        {
            Assert.AreEqual(float.MinValue, range.Min);
            Assert.AreEqual(float.MaxValue, range.Max);

            Assert.AreEqual(range.MinIncluded, range.Contains(float.MinValue));
            Assert.AreEqual(range.MaxIncluded, range.Contains(float.MaxValue));
        }

        //--------------------------------------------------------------------

        [Test]
        public void Probability()
        {
            Range<double> probability = 0.0 <= new Range<double>() <= 1.0;

            Assert.AreEqual(0.0, probability.Min);
            Assert.IsTrue(probability.MinIncluded);
            Assert.AreEqual(1.0, probability.Max);
            Assert.IsTrue(probability.MaxIncluded);

            Assert.IsTrue(probability.Contains(0.0));
            Assert.IsTrue(probability.Contains(1.0));
            Assert.IsTrue(probability.Contains(0.2));

            Assert.IsFalse(probability.Contains(1.0000001));
            Assert.IsFalse(probability.Contains(-0.0000001));
        }

        //--------------------------------------------------------------------

        [Test]
        public void TestScore()
        {
            Range<short> testScore = 0 <= new Range<short>() <= 100;

            short[] validScores = new short[]{ 0, 100, 99, 1, 45, 76 };
            foreach (short score in validScores)
                Assert.IsTrue(testScore.Contains(score));

            short[] invalidScores = new short[]{ -1, 101, 1000, -456 };
            foreach (short score in invalidScores)
                Assert.IsFalse(testScore.Contains(score));
        }
    }
}
