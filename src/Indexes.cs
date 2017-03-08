// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System.Collections;
using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// A collection of indexes for an array or list.
    /// </summary>
    public class Indexes
        : IEnumerable<int>
    {
        private int start;
        private int increment;
        private int count;

        //--------------------------------------------------------------------

        /// <summary>
        /// The collection of indexes in reverse order.
        /// </summary>
        public Indexes Reverse
        {
            get {
                //  If the count is 0, then just return myself.
                if (count == 0)
                    return this;
                else
                    return new Indexes(start + (count-1)*increment, start);
            }
        }

        //--------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance for an array's indexes.
        /// </summary>
        public Indexes(System.Array array)
        {
            this.increment = 1;
            if (array == null) {
                this.start = 0;
                this.count = 0;
            }
            else {
                if (array.Rank != 1)
                    throw new System.ArgumentException("Array's rank must be 1");

                this.start = array.GetLowerBound(0);
                this.count = array.Length;
            }
        }

        //--------------------------------------------------------------------

        /// <summary>
        /// Creates a collection of the indexes for an array.
        /// </summary>
        public static Indexes Of(System.Array array)
        {
            return new Indexes(array);
        }

        //--------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance for a list's indexes.
        /// </summary>
        public Indexes(System.Collections.IList list)
        {
            this.start = 0;
            this.increment = 1;
            if (list == null)
                this.count = 0;
            else
                this.count = list.Count;
        }

        //--------------------------------------------------------------------

        /// <summary>
        /// Creates a collection of the indexes for a list.
        /// </summary>
        public static Indexes Of(System.Collections.IList list)
        {
            return new Indexes(list);
        }

        //--------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance for a range of indexes.
        /// </summary>
        /// <remarks>
        /// If start is greater than end, then the indexes in the collection
        /// are in decreasing order: start, start-1, start-2, ..., end+1, end.
        /// </remarks>
        public Indexes(int start,
                       int end)
        {
            this.start = start;
            if (start > end) {
                this.increment = -1;
                this.count = start - end + 1;
            }
            else {
                this.increment = 1;
                this.count = end - start + 1;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a collection for a range of indexes.
        /// </summary>
        public static Indexes Between(int start,
                                      int end)
        {
            return new Indexes(start, end);
        }

        //---------------------------------------------------------------------

        public IEnumerator<int> GetEnumerator()
        {
            int index = start;
            for (int i = count; i > 0; --i) {
                yield return index;
                index += increment;
            }
        }

        //---------------------------------------------------------------------

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
