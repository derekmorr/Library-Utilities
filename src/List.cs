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

using Generic = System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Methods for working with lists.
    /// </summary>
    public static class List
    {
        /// <summary>
        /// Creates a copy of a list with the items in random order.
        /// </summary>
        /// <param name="list">
        /// The list to copy.
        /// </param>
        /// <param name="randNumGen">
        /// An object that generates random numbers.
        /// </param>
        /// <returns>
        /// A copy of the list with its items shuffled (their order in the
        /// list is random).
        /// </returns>
        public static Generic.IList<T> Shuffle<T>(Generic.IList<T> list,
                                                  System.Random    randomNumGen)
        {
            if (list == null)
                return null;
            Require.ArgumentNotNull(randomNumGen);

            //  List of item indexes.
            Generic.List<int> indexes = new Generic.List<int>(list.Count);
            for (int i = 0; i < list.Count; ++i)
                indexes.Add(i);

            Generic.List<T> shuffledList = new Generic.List<T>(list.Count);
            while (indexes.Count > 0) {
                //  Randomly pick a remaining item in the list
                int indexOfIndex = randomNumGen.Next(indexes.Count);
                int index = indexes[indexOfIndex];
                indexes.RemoveAt(indexOfIndex);
                shuffledList.Add(list[index]);
            }

            return shuffledList;
        }
    }
}
