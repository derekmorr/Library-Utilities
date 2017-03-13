// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Landis.Utilities
{
    public static class Bool
    {
        static Bool()
        {
            Type.SetDescription<bool>("yes/no value");
        }

        //---------------------------------------------------------------------

        public static bool[,] Make2DimArray(string[] rows,
                                            string   trueChars)
        {
            int rowCount = rows.Length;
            if (rowCount == 0)
                return new bool[0,0];
            int columnCount = rows[0].Length;
            bool[,] result = new bool[rowCount, columnCount];
            for (int row = 0; row < rowCount; ++row) {
                string currentRow = rows[row];
                if (currentRow.Length != columnCount) {
                    string mesg = string.Format("The length of row {0} is {1}"
                                                + " instead of {2}", row,
                                                currentRow.Length,
                                                columnCount);
                    throw new System.ArgumentException(mesg);
                }
                for (int column = 0; column < columnCount; ++column)
                    if (trueChars.IndexOf(currentRow[column]) > -1)
                        result[row, column] = true;
            }
            return result;
        }

        //---------------------------------------------------------------------

        //  Read a 2-D boolean array from a file.
        //  File format:
        //      blank lines (empty or just whitespace) & comment lines are
        //          ignored.  Comment line has "#" as first non-whitespace
        //          character.
        //      first data line:  true-chars ={CHARS}
        //          whitespace allowed before & after keyword "true-chars"
        //          all characters after = and before EOL are true chars
        //      0 or more row data lines:  row #? ={CHARS}
        //          whitespace allowed before & after keyword "row",
        //          # is 0 or more digits; allows for numbering for user's
        //          benefit, but #'s are not interpreted by program
        //          row's data are all chars after "=" upto EOL
        //          all rows must have same number of data chars
        public static bool[,] Read2DimArray(string path)
        {
            LineReader reader = new FileLineReader(path);
            reader.SkipBlankLines = true;
            reader.SkipCommentLines = true;

            string trueChars = null;
            string line = reader.ReadLine();
            if (line != null) {
                Regex pattern = new Regex(@"^\s*true-chars\s*=(.*)$");
                Match match = pattern.Match(line);
                if (match.Success)
                    trueChars = match.Groups[1].Value;
            }
            if (trueChars == null)
                throw new LineReaderException(reader,
                                              "Expected a line starting with \"true-chars =\"");

            //  Read row data-lines.
            try {
                Regex pattern = new Regex(@"^\s*row\s*\d*\s*=(.*)$");
                List<string> rows = new List<string>();
                int expectedRowLength = 0;
                while ((line = reader.ReadLine()) != null) {
                    Match match = pattern.Match(line);
                    if (! match.Success)
                        throw new LineReaderException(reader,
                                                      "Expected a line starting with \"row =\" or \"row # =\"");
                    string row = match.Groups[1].Value;
                    rows.Add(row);
                    if (rows.Count == 1) {
                        expectedRowLength = row.Length;
                    }
                    else if (row.Length != expectedRowLength) {
                        string pluralSuffix = expectedRowLength == 1 ? ""
                                                                     : "s";
                        throw new LineReaderException(reader,
                                                      "Expected {0} character{1} after the \"=\"",
                                                      expectedRowLength,
                                                      pluralSuffix);
                    }
                }  // while lines from stream
                return Make2DimArray(rows.ToArray(), trueChars);
            }
            catch (LineReaderException) {
                throw;
            }
            catch (System.Exception e) {
                throw new LineReaderException(reader, e.Message);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Parses a word to see if it's a "yes" or "no" value.
        /// </summary>
        /// <returns>
        /// true if the word is "y" or "yes" (ignoring case); false if the
        /// word is "n", "no", or "-".
        /// </returns>
        /// <exception cref="System.ApplicationException">
        /// The word is not a valid yes/no value.
        /// </exception>
        public static bool ParseYesNo(string word)
        {
            if (word != null) {
                string wordLower = word.ToLower();
                if (wordLower == "y" || wordLower == "yes")
                    return true;
                if (wordLower == "n" || wordLower == "no" || wordLower == "-")
                    return false;
            }
            throw new System.ApplicationException(string.Format("Expected \"yes\", \"y\", \"no\", \"n\", or \"-\"; found \"{0}\" instead.",
                                                                word));
        }
    }
}
