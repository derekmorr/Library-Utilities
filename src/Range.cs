// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// A range of values.  The type T must have two public constant fields
    /// called "MinValue" and "MaxValue" which are both of type T.
    /// </summary>
    public class Range<T>
        where T : IComparable<T>
    {
        private T min;
        private T max;
        private bool minIncluded;
        private bool maxIncluded;

        //---------------------------------------------------------------------

        /// <summary>
        /// The minimum value of the range.
        /// </summary>
        public T Min
        {
            get {
                return min;
            }
            set {
                if (value.CompareTo(max) > 0)
                    throw new ArgumentException("Trying to set min to a value > max.");
                min = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the minimum value is part of the range.
        /// </summary>
        /// <remarks>
        /// An example range of real numbers is (0.0, 1.0], which includes all
        /// the numbers which are greater than 0, and which are less than or
        /// equal to 1.  This range's minimum value is 0.0, but this value is
        /// not part of the range.
        /// </remarks>
        public bool MinIncluded
        {
            get {
                return minIncluded;
            }
            set {
                minIncluded = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        public T Max
        {
            get {
                return max;
            }
            set {
                if (value.CompareTo(min) < 0)
                    throw new ArgumentException("Trying to set max to a value < min.");
                max = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the maximum value is part of the range.
        /// </summary>
        /// <remarks>
        /// An example range of real numbers is [0.0, 100.0), which includes
        /// all the numbers which are equal to or greater than 0, and which are
        /// less than 100.  This range's maximum value is 100.0, but this value
        /// is not part of the range.
        /// </remarks>
        public bool MaxIncluded
        {
            get {
                return maxIncluded;
            }
            set {
                maxIncluded = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with a range that includes all possible
        /// values for type T.
        /// </summary>
        public Range()
        {
            min = Util.Type.GetMinValue<T>();
            max = Util.Type.GetMaxValue<T>();
            minIncluded = true;
            maxIncluded = true;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Determines if the range contains a specified value.
        /// </summary>
        /// <param name="value">
        /// The value to check if it lies within the range.
        /// </param>
        public bool Contains(T value)
        {
            int compareMinResult = value.CompareTo(min);
            if (minIncluded) {
                if (compareMinResult < 0)
                    return false;
            }
            else {
                //  min not included in range
                if (compareMinResult <= 0)
                    return false;
            }
            int compareMaxResult = value.CompareTo(max);
            if (maxIncluded) {
                if (compareMaxResult > 0)
                    return false;
            }
            else {
                //  max not included in range
                if (compareMaxResult >= 0)
                    return false;
            }
            return true;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the maximum value for a range (value is in the range).
        /// </summary>
        public static Range<T> operator<=(Range<T> range,
                                          T        max)
        {
            range.Max = max;
            range.MaxIncluded = true;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the maximum value for a range (value is in the range).
        /// </summary>
        public static Range<T> operator>=(T        max,
                                          Range<T> range)
        {
            range.Max = max;
            range.MaxIncluded = true;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the maximum value for a range (value is not in the range).
        /// </summary>
        public static Range<T> operator<(Range<T> range,
                                         T        max)
        {
            range.Max = max;
            range.MaxIncluded = false;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the maximum value for a range (value is not in the range).
        /// </summary>
        public static Range<T> operator>(T        max,
                                         Range<T> range)
        {
            range.Max = max;
            range.MaxIncluded = false;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the minimum value for a range (value is in the range).
        /// </summary>
        public static Range<T> operator>=(Range<T> range,
                                          T        min)
        {
            range.Min = min;
            range.MinIncluded = true;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the minimum value for a range (value is in the range).
        /// </summary>
        public static Range<T> operator<=(T        min,
                                          Range<T> range)
        {
            range.Min = min;
            range.MinIncluded = true;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the minimum value for a range (value is not in the range).
        /// </summary>
        public static Range<T> operator>(Range<T> range,
                                         T        min)
        {
            range.Min = min;
            range.MinIncluded = false;
            return range;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets the minimum value for a range (value is not in the range).
        /// </summary>
        public static Range<T> operator<(T        min,
                                         Range<T> range)
        {
            range.Min = min;
            range.MinIncluded = false;
            return range;
        }
    }
}
