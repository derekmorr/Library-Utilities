// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;
using System.IO;
using Reflection = System.Reflection;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Information related NUnit testing framework.
    /// </summary>
    public class NUnitInfo
    {
        /// <summary>
        /// The environment variable that specifies the directory where
        /// NUnit data files are built.
        /// </summary>
        public const string DataDirEnvVariable = "NUNIT_DATA_DIR";

        //---------------------------------------------------------------------

        /// <summary>
        /// The environment variable that specifies the directory where
        /// the output from NUnit test is written.
        /// </summary>
        public const string OutDirEnvVariable = "NUNIT_OUT_DIR";

        //---------------------------------------------------------------------

        private Reflection.Assembly callingAssembly;

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the NUnit data directory for the assembly where the instance
        /// resides.  This directory is {NUNIT_DATA_DIR}/{name} where
        /// NUNIT_DATA_DIR is an environment variable, and {name} is
        /// the calling assembly's simple name.
        /// </summary>
        public string GetDataDir()
        {
            return GetDir(DataDirEnvVariable);
        }

        //---------------------------------------------------------------------

        private string GetDir(string envVariable)
        {
            string nunitDir = Environment.GetEnvironmentVariable(envVariable);
            if (nunitDir == null)
                throw new ApplicationException(string.Format("The environment variable {0} is not set.",
                                                             envVariable));
            return Path.Combine(nunitDir, callingAssembly.GetName().Name);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the NUnit output directory for the assembly where the instance
        /// resides.  This directory is {NUNIT_OUT_DIR}/{name} where
        /// NUNIT_OUT_DIR is an environment variable, and {name} is
        /// the calling assembly's simple name.
        /// </summary>
        public string GetOutDir()
        {
            return GetDir(OutDirEnvVariable);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the NUnit output directory for the assembly where the instance
        /// resides.  This directory is {NUNIT_OUT_DIR}/{name} where
        /// NUNIT_OUT_DIR is an environment variable, and {name} is
        /// the calling assembly's simple name.
        /// </summary>
        /// <param name="ensureExists">
        /// Determines whether the method should ensure that the NUnit output
        /// directory.  The directory is created if it does not exist.
        /// </param>
        /// <returns>
        /// null if the environment variable NUNIT_OUT_DIR refers to a file,
        /// or if the directory could not be created.  In either case, an
        /// explanatory message is printed to Console.Error.
        /// </returns>
        public string GetOutDir(bool ensureExists)
        {
            string nunitOutDir = GetOutDir();
            if (! ensureExists)
                return nunitOutDir;

            try {
                Directory.EnsureExists(nunitOutDir);
                return nunitOutDir;
            }
            catch (MultiLineException) {
                Console.Error.WriteLine("Could not create directory named by environment variable {0}",
                                        OutDirEnvVariable);
                return null;
            }
            catch (ApplicationException) {
                Console.Error.WriteLine("The environment variable {0} refers to a file, not a directory.",
                                        OutDirEnvVariable);
                return null;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets a System.IO.TextWriter to which NUnit tests can write console
        /// output.
        /// </summary>
        /// <remarks>
        /// This method will attempt to create a TextWriter for the file called
        /// "{nunit-out-dir}/console-output.txt" where {nunit-out-dir} is the
        /// NUnit output directory for the calling assembly.  If this
        /// TextWriter cannot be opened, this method returns the
        /// TextWriter for console's standard output (System.Console.Out).
        /// Also, if NUnit is running in GUI mode (determined by looking at
        /// the command line), System.Console.Out is returned.
        /// </remarks>
        public TextWriter GetTextWriter()
        {
            if (Environment.CommandLine.ToLower().Contains("nunit.exe"))
                return Console.Out;

            string outDir = GetOutDir(true);
            if (outDir == null)
                return Console.Out;

            string outputFile = Path.Combine(outDir, "console-output.txt");
            try {
                StreamWriter writer = new StreamWriter(outputFile);
                writer.AutoFlush = true;
                return writer;
            }
            catch (System.Exception) {
                return Console.Out;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes an instance.
        /// </summary>
        public NUnitInfo()
        {
            callingAssembly = Reflection.Assembly.GetCallingAssembly();
        }
    }
}
