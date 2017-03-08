// Copyright 2004 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System.Text;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Methods for working with strings.
    /// </summary>
    public static class String
    {
        static String()
        {
            Type.SetDescription<string>("text");
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Read a string from a StringReader.
        /// </summary>
        /// <remarks>
        /// This method reads a string from a StringReader.  The string is
        /// either an unquoted word or a quoted string.  A word is one or more
        /// adjacent non-whitespace characters.  A quoted string is zero or
        /// more characters surrounded by a pair of quotes.  The quotes may be
        /// a pair of double quotes (") or a pair of single quotes (').  A
        /// quote character can be included inside a quoted string by escaping
        /// it with a backslash (\).
        /// <example>
        /// Here are some valid strings:
        /// <code>
        ///   foo
        ///   a-brief-phrase
        ///   C:\some\path\to\a\file.ext
        ///   ""
        ///   "Four score and seven years ago ..."
        ///   "That's incredulous!"
        ///   'That\'s incredulous!'
        ///   ''
        ///   'He said "Boo."'
        ///   "He said \"Boo.\""
        /// </code>
        /// </example>
        /// Whitespace preceeding the word or the quoted string is skipped.
        /// The delimiting quotes of a quoted string are removed.
        /// </remarks>
        /// <exception cref="">System.IO.EndOfStreamException</exception>
        public static InputValue<string> Read(StringReader reader,
                                              out int      index)
        {
            TextReader.SkipWhitespace(reader);
            if (reader.Peek() == -1)
                throw new InputValueException();

            index = reader.Index;
            char nextChar = (char) reader.Peek();
            if (nextChar == '\'' || nextChar == '"') {
                //  Quoted string
                char startQuote = (char) reader.Read();
                StringBuilder quotedStr = new StringBuilder();
                quotedStr.Append(startQuote);
                StringBuilder actualStr = new StringBuilder();
                bool endQuoteFound = false;
                while (! endQuoteFound) {
                    char? ch = TextReader.ReadChar(reader);
                    if (! ch.HasValue)
                        throw new InputValueException(quotedStr.ToString(),
                                                      "String has no end quote: {0}",
                                                      quotedStr.ToString());
                    if (ch.Value == startQuote) {
                        endQuoteFound = true;
                        quotedStr.Append(ch.Value);
                    }
                    else {
                        if (ch.Value == '\\') {
                            //  Get the next character if it's a quote.
                            nextChar = (char) reader.Peek();
                            if (nextChar == '\'' || nextChar == '"') {
                                quotedStr.Append(ch.Value);
                                ch = TextReader.ReadChar(reader);
                            }
                        }
                        actualStr.Append(ch.Value);
                        quotedStr.Append(ch.Value);
                    }
                }
                return new InputValue<string>(actualStr.ToString(),
                                              quotedStr.ToString());
            }
            else {
                string word = TextReader.ReadWord(reader);
                return new InputValue<string>(word, word);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Prepends an indefinite article ("a" or "an") to a noun.
        /// </summary>
        /// <remarks>
        /// If the noun is capitalized (first letter is uppercase, rest
        /// lowercase), then the article is also capitalized (e.g.,
        /// "An Important Idea").  If the noun is all uppercase, the article
        /// is too (e.g., "AN ORANGE").  Otherwise, the article is lowercase.
        /// </remarks>
        public static string PrependArticle(string noun)
        {
            if (string.IsNullOrEmpty(noun))
                return noun;
            string nounUpper = noun.ToUpper();
            string article = ("AEIOU".IndexOf(nounUpper[0]) >= 0) ? "an"
                                                                  : "a";
            if (noun == nounUpper)
                article = article.ToUpper();
            else if (noun.Length > 1 && char.IsUpper(noun, 0) && char.IsLower(noun, 1))
                article = (article == "a") ? "A" : "An";
            return article + " " + noun;
        }
    }
}
