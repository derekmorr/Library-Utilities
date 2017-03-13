// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;
using System.Collections.Generic;
using System.Text;

namespace Landis.Utilities
{
    /// <summary>
    /// Methods for working with macros.
    /// </summary>
    /// <remarks>
    /// A macro has a name and a value, both of which are strings.  When a
    /// macro's name is used in a string, it is surrounded by curly braces,
    /// for example, "Hi {first-name}!".  There is a special macro called
    /// "LEFT-BRACE" which can be used to insert a left curly brace into a
    /// string.  For example, if the string "The '{LEFT-BRACE}' key is not
    /// working." has its macro replaced, the result will be "The '{' key is
    /// not working.".
    /// </remarks>
    public static class Macros
    {
        /// <summary>
        /// Gets all the macro names that are used in a string.
        /// </summary>
        /// <param name="str">
        /// The string to scan for macro names.
        /// </param>
        /// <returns>
        /// List of names in the string in the order they are used.  If a name
        /// is used more than once in the string, it appears in the list only
        /// once.  Special macros are excluded from the list.
        /// </returns>
        public static IList<string> GetNames(string str)
        {
            List<string> names = new List<string>();
            int index = 0;
            while (index < str.Length) {
                int leftBraceIndex = str.IndexOf('{', index);
                if (leftBraceIndex == -1)
                    //  No macros left
                    index = str.Length;
                else {
                    int nameStartIndex = leftBraceIndex + 1;
                    if (nameStartIndex == str.Length)
                        //  Left brace is last character
                        index = str.Length;
                    else {
                        int rightBraceIndex = str.IndexOf('}', nameStartIndex);
                        if (rightBraceIndex == -1)
                            //  No matching right brace found before end of string
                            index = str.Length;
                        else {
                            string name = str.Substring(nameStartIndex, rightBraceIndex - nameStartIndex);
                            if (name != "LEFT-BRACE") {
                                if (! names.Contains(name))
                                    names.Add(name);
                            }
                            index = rightBraceIndex + 1;
                        }
                    }
                }
            }
            return names;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Replaces all the macros in a string with their values.
        /// </summary>
        /// <param name="str">
        /// The string to scan for macros.
        /// </param>
        /// <param name="macros">
        /// The macro values.
        /// </param>
        /// <returns>
        /// The string with all the macros replaced with their values.
        /// </returns>
        /// <exception cref="System.ApplicationException">
        /// An unknown macro was found in the string.  The macro's name is
        /// stored in the exception's Data property under the key "Name".
        /// The macro's name with the surrounding curly braces is in the
        /// Data property under the key "DelimitedName".
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// One or more of the parameters is null.
        /// </exception>
        /// <remarks>
        /// Each occurrence of the special macro "LEFT-BRACE" is replaced with
        /// a "{".
        /// </remarks>
        public static string Replace(string                      str,
                                     IDictionary<string, string> macros)
        {
            Require.ArgumentNotNull(str);
            Require.ArgumentNotNull(macros);

            StringBuilder result = new StringBuilder();

            int index = 0;
            while (index < str.Length) {
                int leftBraceIndex = str.IndexOf('{', index);
                if (leftBraceIndex == -1) {
                    //  No macros left, so copy rest of string
                    result.Append(str.Substring(index));
                    index = str.Length;
                }
                else {
                    int nameStartIndex = leftBraceIndex + 1;
                    if (nameStartIndex == str.Length) {
                        //  Left brace is last character, so copy rest of string
                        result.Append(str.Substring(index));
                        index = str.Length;
                    }
                    else {
                        int rightBraceIndex = str.IndexOf('}', nameStartIndex);
                        if (rightBraceIndex == -1) {
                            //  No matching right brace found before end of string
                            result.Append(str.Substring(index));
                            index = str.Length;
                        }
                        else {
                            //  Copy text preceeding macro name
                            if (leftBraceIndex > index)
                                result.Append(str.Substring(index, leftBraceIndex - index));

                            string name = str.Substring(nameStartIndex, rightBraceIndex - nameStartIndex);
                            string macroValue;
                            try {
                                macroValue = macros[name];
                            }
                            catch (Exception) {
                                if (name == "LEFT-BRACE")
                                    macroValue = "{";
                                else {
                                    string delimitedName = "{" + name + "}";
                                    ApplicationException exc = new System.ApplicationException("Unknown macro: " + delimitedName);
                                    exc.Data["Name"] = name;
                                    exc.Data["DelimitedName"] = delimitedName;
                                    throw exc;
                                }
                            }
                            result.Append(macroValue);
                            index = rightBraceIndex + 1;
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
