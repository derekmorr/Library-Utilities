using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Edu.Wisc.Forest.Flel.Util.PlugIns
{
    /// <summary>
    /// Methods for loading plug-ins.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// Loads a plug-in.
        /// </summary>
        /// <param name="T">
        /// The plug-in's interface.
        /// </param>
        /// <param name="info">
        /// Information about the plug-in to be loaded.
        /// </param>
        public static T Load<T>(IInfo info)
        {
            System.Type plugInImplType;
            try {
                plugInImplType = System.Type.GetType(info.ImplementationName);
            }
            catch (System.Exception exc) {
                if (string.IsNullOrEmpty(info.ImplementationName))
                    throw LoadException(info,
                                        "The plug-in has no implementation associated with it.");
                throw LoadException(info,
                                    "Cannot get the data type that implements the plug-in:",
                                    "  Data type:  " + info.ImplementationName,
                                    "  Error:      " + exc.Message);
            }
            if (plugInImplType == null)
                throw LoadException(info,
                                    "Cannot get the data type that implements the plug-in:",
                                    "  Data type:  " + info.ImplementationName,
                                    "  Error:      No data type with that name is installed.");

            return Load<T>(info.Name, plugInImplType);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Loads a plug-in.
        /// </summary>
        /// <param name="T">
        /// The plug-in's interface.
        /// </param>
        /// <param name="name">
        /// The plug-in's name.
        /// </param>
        /// <param name="implementationType">
        /// The class that implements the plug-in.
        /// </param>
        public static T Load<T>(string      name,
                                System.Type implementationType)
        {
            string[] errMesg;
            try {
                System.Reflection.Assembly assembly = implementationType.Assembly;
                T plugIn = (T) assembly.CreateInstance(implementationType.FullName);
                if (plugIn != null)
                    return plugIn;
                errMesg = new string[]{"Could not create an instance of the plug-in."};
            }
            catch (System.InvalidCastException) {
                errMesg = new string[]{"The plug-in does not support the proper interface: " + typeof(T).FullName};
            }
            catch (System.Exception exc) {
                errMesg = new string[]{"Could not create an instance of the plug-in:",
                                       "  " + exc.Message};
            }
            throw LoadException(new Info(name, typeof(T), implementationType.AssemblyQualifiedName),
                                errMesg);
        }

        //---------------------------------------------------------------------

        private static Exception LoadException(IInfo           plugIn,
                                               params string[] messageLines)
        {
            return LoadException(plugIn, new MultiLineText(messageLines));
        }

        //---------------------------------------------------------------------

        private static Exception LoadException(IInfo         plugIn,
                                               MultiLineText message)
        {
            return new Exception(plugIn, "Error while loading the plug-in",
                                 message);
        }
    }
}
