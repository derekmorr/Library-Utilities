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

using System;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// A percentage value.
    /// </summary>
    public class Percentage
        : IComparable, IFormattable
    {
        //  Minimum and maximum values are the corresponding limits of
        //  System.Double divided by 100 because the Parse and ToString
        //  methods have to be able to represent a percentage's value
        //  multiplied by 100 as a double.
        public const double MinValueAsDouble = double.MinValue/100;
        public const double MaxValueAsDouble = double.MaxValue/100;
        public static readonly Percentage MinValue;
        public static readonly Percentage MaxValue;

        //---------------------------------------------------------------------

        private double percentage;

        //---------------------------------------------------------------------

        static Percentage()
        {
            Util.Type.SetDescription<Percentage>("percentage");
            InputValues.Register<Percentage>(Percentage.Parse);

             MinValue = new Percentage(MinValueAsDouble);
             MaxValue = new Percentage(MaxValueAsDouble);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The percentage as a numeric value.
        /// </summary>
        public double Value
        {
            get {
                return percentage;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance to 0%.
        /// </summary>
        public Percentage()
        {
            percentage = 0.0;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance to a specific numeric value.
        /// </summary>
        /// <exception cref="System.OverflowException">
        /// The value is less than MinValue or greater than MaxValue.
        /// </exception>
        public Percentage(double value)
        {
            if (value < MinValueAsDouble || value > MaxValueAsDouble)
                throw new OverflowException();
            percentage = value;
        }

        //---------------------------------------------------------------------


        /// <summary>
        /// Converts a percentage value to a double.
        /// </summary>
        public static implicit operator double(Percentage percentage)
        {
            return percentage.Value;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a string into a percentage value.
        /// </summary>
        /// <remarks>
        /// The string may have whitespace preceeding the percentage value.
        /// The value is a double-precision number followed by a "%" character.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// str is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// str does not have a valid format.
        /// </exception>
        /// <exception cref="System.OverflowException">
        /// str represents a value smaller than Double.MinValue or larger
        /// than Double.MaxValue.
        /// </exception>
        public static Percentage Parse(string str)
        {
            Require.ArgumentNotNull(str);
            str = str.Trim(null);
            if (! str.EndsWith("%"))
                throw new FormatException("Missing \"%\" at end of percentage value");
            string justValue = str.Remove(str.Length-1);
            try {
                return new Percentage(double.Parse(justValue)/100);
            }
            catch (FormatException) {
                throw new FormatException(string.Format("\"{0}\" is not a valid number", justValue));
            }
        }

        //---------------------------------------------------------------------

        public int CompareTo(object obj)
        {
            if (obj is Percentage)
                return percentage.CompareTo(((Percentage) obj).percentage);
            if (obj is double)
                return percentage.CompareTo((double) obj);
            throw new ArgumentException("Argument is not Percentage or double");
        }

        //---------------------------------------------------------------------

        public string ToString(string          format,
                               IFormatProvider formatProvider)
        {
            if (format == null)
                return (percentage * 100).ToString(null, formatProvider) + "%";
            format = format.Trim(null);
            if (format.EndsWith("%")) {
                string valueFormat = format.Substring(0, format.Length-1);
                return (percentage * 100).ToString(valueFormat, formatProvider) + "%";
            }
            //  Use format for percentage's numeric value.
            return percentage.ToString(format, formatProvider);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns a string that represents the percentage.
        /// </summary>
        /// <remarks>
        /// If the percentage is 0.5, then this method returns "50%".
        /// </remarks>
        public override string ToString()
        {
            return ToString(null, null);
        }
    }
}
