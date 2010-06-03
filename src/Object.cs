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

namespace Edu.Wisc.Forest.Flel.Util
{
    public static class Object
    {
        public enum CompareResult
        {
            ReferToSame,
            OneIsNull,
            DifferentInstances
        }

        //---------------------------------------------------------------------

        public static CompareResult Compare(object x,
                                            object y)
        {
            int nullCount = 0;
            if (x == null)
                nullCount++;
            if (y == null)
                nullCount++;
            switch (nullCount) {
                case 1:
                    return CompareResult.OneIsNull;
                case 2:
                    return CompareResult.ReferToSame;
            }
            if (x == y)
                return CompareResult.ReferToSame;
            else
                return CompareResult.DifferentInstances;
        }
    }
}
