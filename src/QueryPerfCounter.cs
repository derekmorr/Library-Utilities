// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

//  Code from the How To guide titled "How To: Time Managed Code Using
//  QueryPerformanceCounter and QueryPerformanceFrequency" at the MSDN
//  library:
//
//    .NET Development
//    > Improving .NET Application Performance and Scalability
//      >  How Tos
//
//  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnpag/html/scalenethowto09.asp
//
//  QueryPerfCounter.cs

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Edu.Wisc.Forest.Flel.Util
{
    public class QueryPerfCounter
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long start;
        private long stop;
        private long frequency;
        Decimal multiplier = new Decimal(1.0e9);

        //---------------------------------------------------------------------

        public QueryPerfCounter()
        {
            if (QueryPerformanceFrequency(out frequency) == false) {
                // Frequency not supported
                throw new Win32Exception();
            }
        }

        //---------------------------------------------------------------------

        public void Start()
        {
            QueryPerformanceCounter(out start);
        }

        //---------------------------------------------------------------------

        public void Stop()
        {
            QueryPerformanceCounter(out stop);
        }

        //---------------------------------------------------------------------

        public double Duration(int iterations)
        {
            return ((((double)(stop - start)* (double) multiplier) / (double) frequency)/iterations);
        }
    }
}
