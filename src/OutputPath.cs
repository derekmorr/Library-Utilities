using System;
using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Methods for working with paths of output files.
    /// </summary>
    public static class OutputPath
    {
        /// <summary>
        /// Checks a template for output filenames to ensure that all the
        /// variables used in the template are known.
        /// </summary>
        /// <param name="template">
        /// The template for a set of output filenames.
        /// </param>
        /// <param name="variables">
        /// A dictionary collection of known variables where a key is the
        /// variable's name and its value indicates whether the variable
        /// is required or not.
        /// </param>
        /// <remarks>
        /// A variable is used by enclosing its name in curly braces, e.g.,
        /// "{variable-name}".
        /// </remarks>
        /// <exception cref="InputValueException">
        /// The template is missing a required variable or has unknown
        /// variable.
        /// </exception>
        public static void CheckTemplateVars(string                    template,
                                             IDictionary<string, bool> variables)
        {
            IList<string> namesUsed = Macros.GetNames(template);

            IList<string> unknownVars = new List<string>();
            foreach (string name in namesUsed) {
                if (! variables.ContainsKey(name))
                    unknownVars.Add(name);
            }
            if (unknownVars.Count == 1) {
                string mesg = string.Format("The template uses an unknown variable: {{{0}}}",
                                            unknownVars[0]);
                throw new InputValueException(template, mesg);
            }
            if (unknownVars.Count > 1) {
                MultiLineText innerMesg = new MultiLineText();
                foreach (string name in unknownVars)
                    innerMesg.Add(string.Format("{{{0}}}", name));
                throw new InputValueException(template,
                                              "The template uses these unknown variables",
                                              innerMesg);
            }

            //  No unknown variables; check if all required variables were used
            IList<string> missingReqdVars = new List<string>();
            foreach (string name in variables.Keys) {
                if (variables[name]) {
                    //  Required
                    if (! namesUsed.Contains(name))
                        missingReqdVars.Add(name);
                }
            }
            if (missingReqdVars.Count == 1) {
                MultiLineText mesg = new MultiLineText();
                mesg.Add(string.Format("The template must include the variable {{{0}}} to ensure",
                                       missingReqdVars[0]));
                mesg.Add("that the names of all output files are unique.");
                throw new InputValueException(template, mesg);
            }
            if (missingReqdVars.Count > 1) {
                MultiLineText mesg = new MultiLineText();
                mesg.Add("The template must use the following variables to ensure");
                mesg.Add("that the names of all output files are unique:");

                MultiLineText innerMesg = new MultiLineText();
                foreach (string name in missingReqdVars)
                    innerMesg.Add(string.Format("{{{0}}}", name));
                throw new InputValueException(template, mesg, innerMesg);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Replaces all the variables in a template to yield an output
        /// filename.
        /// </summary>
        /// <param name="template">
        /// The template for a set of output filenames.
        /// </param>
        /// <param name="variables">
        /// A dictionary collection of known variables and their values.
        /// </param>
        /// <remarks>
        /// A variable is used by enclosing its name in curly braces, e.g.,
        /// "{variable-name}".
        /// </remarks>
        /// <exception cref="System.ApplicationException">
        /// The template has an unknown variable.  The Data property of the
        /// exception has these keys:
        ///
        ///   "Template" --> the template parameter passed to the method
        ///   "Variable" --> the name of unknown variable
        /// </exception>
        public static string ReplaceTemplateVars(string                      template,
                                                 IDictionary<string, string> variables)
        {
            try {
                return Macros.Replace(template, variables);
            }
            catch (ApplicationException exc) {
                string mesg = string.Format("The filename template \"{0}\" has an unknown variable: {1}",
                                            template, exc.Data["DelimitedName"]);
                ApplicationException newExc = new ApplicationException(mesg);
                newExc.Data["Template"] = template;
                newExc.Data["Variable"] = exc.Data["Name"];
                throw newExc;
            }
        }
    }
}
