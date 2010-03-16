namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Wrapper for ParseMethod so it can used as a ReadMethod.
    /// </summary>
    public class ParseMethodWrapper<T>
    {
        private ParseMethod<T> parseMethod;

        //---------------------------------------------------------------------

        public ParseMethodWrapper(ParseMethod<T> method)
        {
            this.parseMethod = method;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Read a value from a TextReader.
        /// </summary>
        /// <remarks>
        /// If the wrapped parse method throws an exception, this method
        /// catches the exception, modifies its Data property, and then
        /// rethrows it.  A new key/value pair is set in the Data property:
        /// the key is "ParseMethod.Word", and the value is the word that
        /// was passed as a parameter to the parse method.
        /// </remarks>
        public InputValue<T> Read(StringReader reader,
                                  out int      index)
        {
            //  Read word from reader.  A word is a sequence of 1 or more
            //  non-whitespace characters.
            TextReader.SkipWhitespace(reader);
            if (reader.Peek() == -1)
                throw new InputValueException();

            index = reader.Index;
            string word = TextReader.ReadWord(reader);
            try {
                return new InputValue<T>(parseMethod(word), word);
            }
            catch (System.OverflowException) {
                string format = Type.GetNumericFormat<T>();
                if (format.Length > 0)
                    format = string.Format("{{0:{0}}}", format);
                else
                    format = "{0}";
                string min = string.Format(format, Type.GetMinValue<T>());
                string max = string.Format(format, Type.GetMaxValue<T>());
                string numericDesc = Type.GetNumericDescription<T>();
                numericDesc = String.PrependArticle(numericDesc);
                string message = string.Format("{0} is outside the range for {1}",
                                               word, numericDesc);
                MultiLineText range = new MultiLineText();
                range.Add(string.Format("Range is {0} to {1}", min, max));
                throw new InputValueException(word, message, range);
            }
            catch (System.Exception exc) {
                string message= string.Format("\"{0}\" is not a valid {1}",
                                              word,
                                              Type.GetDescription<T>());
                System.FormatException formatExc = exc as System.FormatException;
                //  Add the format message if it's not a system type (assume
                //  derived type is providing more detailed explanation).
                if (formatExc != null && ! typeof(T).Namespace.StartsWith("System"))
                    throw new InputValueException(word, message,
                                                  new MultiLineText(formatExc.Message));
                else
                    throw new InputValueException(word, message);
            }
        }
    }
}
