using System.Collections.Generic;

namespace Edu.Wisc.Forest.Flel.Util.PlugIns
{
    /// <summary>
    /// Information about a plug-in.
    /// </summary>
    public class Info
        : IInfo
    {
        private string name;
        private System.Type interfaceType;
        private string implementationName;

        //---------------------------------------------------------------------

        public string Name
        {
            get {
                return name;
            }
        }

        //---------------------------------------------------------------------

        public System.Type InterfaceType
        {
            get {
                return interfaceType;
            }
        }

        //---------------------------------------------------------------------

        public string ImplementationName
        {
            get {
                return implementationName;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of plug-in information.
        /// </summary>
        /// <param name="name">Name of the plug-in.</param>
        /// <param name="interfaceType">The type of the plug-in's interface
        /// </param>
        /// <param name="implementationName">The AssemblyQualifiedName of the
        /// class that implements the plug-in.</param>
        public Info(string      name,
                    System.Type interfaceType,
                    string      implementationName)
        {
            this.name = name;
            this.interfaceType = interfaceType;
            this.implementationName = implementationName;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the information for the plug-ins in an assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to be searched.</param>
        /// <param name="plugInInterfaces">
        /// The collection of plug-in interfaces to search for.
        /// </param>
        /// <returns></returns>
        public static IInfo[] GetPlugIns(System.Reflection.Assembly assembly,
                                         ICollection<System.Type>   plugInInterfaces)
        {
            List<IInfo> plugIns = new List<IInfo>();

            foreach (System.Type type in assembly.GetTypes()) {
                //  Plug-ins are implemented by public classes.
                if (! type.IsPublic || ! type.IsClass)
                    continue;

                //  See if any of the type's interfaces is a plug-in interface.
                System.Type plugInInterface = null;
                foreach (System.Type interfc in type.GetInterfaces()) {
                    if (plugInInterfaces.Contains(interfc)) {
                        plugInInterface = interfc;
                        break;
                    }
                }
                if (plugInInterface == null)
                    continue;

                //  Get the plug-in's name, and then store info for plug-in.
                try {
                    IPlugIn plugIn = Loader.Load<IPlugIn>("(name?)", type);
                    plugIns.Add(new Info(plugIn.Name,
                                         plugInInterface,
                                         type.AssemblyQualifiedName));
                }
                catch (Exception) {
                    //  Eventually notify user there was error loading the plug-in.
                }
            }

            return plugIns.ToArray();
        }
    }
}
