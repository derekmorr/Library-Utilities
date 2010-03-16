namespace Edu.Wisc.Forest.Flel.Util.PlugIns
{
    /// <summary>
    /// Information about a plug-in.
    /// </summary>
    public interface IInfo
    {
        /// <summary>
        /// The name that users refer to the plug-in by.
        /// </summary>
        string Name
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The type of the plug-in's interface.
        /// </summary>
        System.Type InterfaceType
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The AssemblyQualifiedName of the class that implements the plug-in.
        /// </summary>
        string ImplementationName
        {
            get;
        }
    }
}
