// Copyright 2005 University of Wisconsin 
// All rights reserved. 
// 
// Contributors: 
//   James Domingo, Forest Landscape Ecology Lab, UW-Madison 

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// Set of methods for getting information about assemblies.
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary>
        /// Gets a list of assemblies loaded in the current application domain.
        /// The list is filtered so that it only contains those assemblies
        /// whose names start with one of the given prefixes.  If the array of
        /// prefixes is null or has no elements, all the loaded assemblies are
        /// returned.
        /// </summary>
        public static Assembly[] GetLoadedAssemblies(string[] prefixes)
        {
            if (prefixes == null)
                prefixes = new string[0];

            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assemblies = currentDomain.GetAssemblies();

            if (prefixes.Length == 0)
                return assemblies;

            List<Assembly> result = new List<Assembly>();
            foreach (Assembly assembly in assemblies) {
                foreach (string prefix in prefixes) {
                    if (assembly.FullName.StartsWith(prefix)) {
                        result.Add(assembly);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        //---------------------------------------------------------------------

        public static void WriteLoadedAssemblies(string[]   prefixes,
                                                 TextWriter writer)
        {
            Assembly[] assemblies = GetLoadedAssemblies(prefixes);
            if (assemblies == null || assemblies.Length == 0)
                return;

            writer.WriteLine("Loaded Assemblies:");
            foreach (Assembly assembly in assemblies) {
                writer.WriteLine("  {0}", assembly.ToString());
            }
        }
    }
}
