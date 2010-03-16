using System;
using System.Collections.Generic;
using System.Globalization;

namespace Edu.Wisc.Forest.Flel.Util
{
    public static class InputValues
    {
        private static Dictionary<string, object> readMethods;
        private static Dictionary<string, object> parseMethods;

        //---------------------------------------------------------------------

        static InputValues()
        {
            readMethods = new Dictionary<string, object>();
            parseMethods = new Dictionary<string, object>();

            NumberStyles integerStyle = NumberStyles.Integer |
                                        NumberStyles.AllowThousands;
            Register<byte, NumberStyles>(byte.Parse, integerStyle);
            Register<sbyte, NumberStyles>(sbyte.Parse, integerStyle);
            Register<short, NumberStyles>(short.Parse, integerStyle);
            Register<ushort, NumberStyles>(ushort.Parse, integerStyle);
            Register<int, NumberStyles>(int.Parse, integerStyle);
            Register<uint, NumberStyles>(uint.Parse, integerStyle);
            Register<long, NumberStyles>(long.Parse, integerStyle);
            Register<ulong, NumberStyles>(ulong.Parse, integerStyle);

            NumberStyles floatStyle = NumberStyles.Float |
                                      NumberStyles.AllowThousands;
            Register<float, NumberStyles>(float.Parse, floatStyle);
            Register<double, NumberStyles>(double.Parse, floatStyle);

            Register<string>(String.Read);
            Register<bool>(Bool.ParseYesNo);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Registers a method to read a value of a specific type from a text
        /// text reader.
        /// </summary>
        public static void Register<T>(ReadMethod<T> method)
        {
            string typeFullName = typeof(T).FullName;
            readMethods[typeFullName] = method;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Registers a method to parse a word for a value of a specific type.
        /// </summary>
        public static void Register<T>(ParseMethod<T> method)
        {
            Register<T>(new ParseMethodWrapper<T>(method).Read);
            string typeFullName = typeof(T).FullName;
            parseMethods[typeFullName] = method;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Registers a 2-parameter method to parse a word for a value of a
        /// specific type.
        /// </summary>
        public static void Register<T, Parameter2Type>(ParseMethod2<T, Parameter2Type> method,
                                                       Parameter2Type                  parameter2)
        {
            Register<T>(new ParseMethod2Wrapper<T, Parameter2Type>(method, parameter2).Parse);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the ReadMethod that has been registered for a type.
        /// </summary>
        public static ReadMethod<T> GetReadMethod<T>()
        {
            try {
                object readMethod = readMethods[typeof(T).FullName];
                return (ReadMethod<T>) readMethod;
            }
            catch (KeyNotFoundException) {
                string mesg = string.Format("No parse or read method registered for {0}",
                                            typeof(T).FullName);
                throw new ApplicationException(mesg);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the ParseMethod that has been registered for a type.
        /// </summary>
        public static ParseMethod<T> GetParseMethod<T>()
        {
            try {
                object parseMethod = parseMethods[typeof(T).FullName];
                return (ParseMethod<T>) parseMethod;
            }
            catch (KeyNotFoundException) {
                string mesg = string.Format("No parse method registered for {0}",
                                            typeof(T).FullName);
                throw new ApplicationException(mesg);
            }
        }
    }
}
