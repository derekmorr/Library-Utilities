using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using System;

namespace Edu.Wisc.Forest.Flel.Test.Util
{
    [TestFixture]
    public class QueryPerfCounter_Test
    //  This test fixture is based on the sample program called
    //  "ValidateQueryPerfCounter.cs" in the How To guide titled
    //  "How To: Time Managed Code Using QueryPerformanceCounter and
    //  QueryPerformanceFrequency" at the MSDN library.
    {
        [Test]
        public void RunTest()
        {
            int iterations = 5;

            // Call the object and methods to JIT before the test run
            QueryPerfCounter myTimer = new QueryPerfCounter();
            myTimer.Start();
            myTimer.Stop();

            // Time the overall test duration
            DateTime dtStartTime = DateTime.Now;

            // Use QueryPerfCounters to get the average time per iteration
            myTimer.Start();

            for (int i = 0; i < iterations; i++) {
                // Method to time
                System.Threading.Thread.Sleep(1000);
            }
            myTimer.Stop();

            // Calculate time per iteration in nanoseconds
            double result = myTimer.Duration(iterations);

            // Show the average time per iteration results
            Data.Output.WriteLine("Iterations: {0}", iterations);
            Data.Output.WriteLine("Average time per iteration: ");
            Data.Output.WriteLine(result/1000000000 + " seconds");
            Data.Output.WriteLine(result/1000000 + " milliseconds");
            Data.Output.WriteLine(result + " nanoseconds");

            // Show the overall test duration results
            DateTime dtEndTime = DateTime.Now;
            Double duration = ((TimeSpan)(dtEndTime-dtStartTime)).TotalMilliseconds;
            Data.Output.WriteLine();
            Data.Output.WriteLine("Duration of test run: ");
            Data.Output.WriteLine(duration/1000 + " seconds");
            Data.Output.WriteLine(duration + " milliseconds");
        }
    }
}
