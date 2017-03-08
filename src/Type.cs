// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using IO = System.IO;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Methods for getting information about types.
    /// </summary>
    public static class Type
    {
        private static Dictionary<string, string> descriptions;

        //---------------------------------------------------------------------

        static Type()
        {
            descriptions = new Dictionary<string, string>();

            SetDescription<byte>("integer");
            SetDescription<sbyte>("integer");
            SetDescription<short>("integer");
            SetDescription<ushort>("integer");
            SetDescription<int>("integer");
            SetDescription<uint>("integer");
            SetDescription<long>("integer");
            SetDescription<ulong>("integer");

            SetDescription<float>("number");
            SetDescription<double>("number");
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Registers a user-friendly description for a type.
        /// </summary>
        /// <param name="description">
        /// Brief user-friendly description for the type.  If null, then any
        /// currently registered description for the type is deleted.
        /// </param>
        /// <remarks>
        /// The following base types have pre-registered descriptions:
        ///
        /// <pre>
        ///   byte
        ///   sbyte
        ///   short
        ///   ushort
        ///   int
        ///   uint
        ///   long
        ///   ulong
        ///   float
        ///   double
        /// </pre>
        /// </remarks>
        public static void SetDescription<T>(string description)
        {
            if (description == null)
                descriptions.Remove(typeof(T).FullName);
            else
                descriptions[typeof(T).FullName] = description;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets a user-friendly description for a type.
        /// </summary>
        /// <returns>
        /// If a description was registered with the SetDescription method for
        /// the type, then that description is returned.  Otherwise, the type's
        /// name is returned.
        /// </returns>
        public static string GetDescription<T>()
        {
            System.Type type = typeof(T);
            string description;
            if (descriptions.TryGetValue(type.FullName, out description))
                return description;
            else
                return type.Name;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets a more detailed description for a base numeric type.
        /// </summary>
        public static string GetNumericDescription<T>()
        {
            string signAndPrecision;
            switch (System.Type.GetTypeCode(typeof(T))) {
                case TypeCode.Byte:
                    signAndPrecision = "unsigned 8-bit";
                    break;
                case TypeCode.SByte:
                    signAndPrecision = "8-bit";
                    break;
                case TypeCode.Int16:
                    signAndPrecision = "16-bit";
                    break;
                case TypeCode.Int32:
                    signAndPrecision = "32-bit";
                    break;
                case TypeCode.Int64:
                    signAndPrecision = "64-bit";
                    break;
                case TypeCode.UInt16:
                    signAndPrecision = "unsigned 16-bit";
                    break;
                case TypeCode.UInt32:
                    signAndPrecision = "unsigned 32-bit";
                    break;
                case TypeCode.UInt64:
                    signAndPrecision = "unsigned 64-bit";
                    break;
                case TypeCode.Single:
                    signAndPrecision = "single-precision";
                    break;
                case TypeCode.Double:
                    signAndPrecision = "double-precision";
                    break;

                default:
                    return GetDescription<T>();
            }
            return signAndPrecision + " " + GetDescription<T>();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the value of a type's static field.
        /// </summary>
        /// <param name="fieldName">
        /// Name of the field.
        /// </param>
        public static TConstant GetStaticField<T,TConstant>(string fieldName)
        {
            System.Type type = typeof(T);
            System.Reflection.FieldInfo field = type.GetField(fieldName);
            if (field != null && field.IsStatic
                              && field.FieldType == typeof(TConstant))
                return (TConstant) field.GetValue(null);
            throw new System.InvalidOperationException(string.Format("Cannot get static field {0} for type {1}", fieldName, type.FullName));
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get the type's static field named "MinValue".
        /// </summary>
        public static T GetMinValue<T>()
        {
            return GetStaticField<T,T>("MinValue");
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Get the type's static field named "MaxValue".
        /// </summary>
        public static T GetMaxValue<T>()
        {
            return GetStaticField<T,T>("MaxValue");
        }

        //---------------------------------------------------------------------

        public static string GetNumericFormat<T>()
        {
            switch (System.Type.GetTypeCode(typeof(T))) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return "#,##0";

                case TypeCode.Single:
                case TypeCode.Double:
                    return "g";

                default:
                    try {
                        return GetStaticField<T,string>("NumericFormat");
                    }
                    catch (System.InvalidOperationException) {
                        return "";
                    }
            }
        }
    }
}
