﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimTuning.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SimTuning.Core.resources", typeof(resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No acces to GPS.
        /// </summary>
        public static string ERR_LOCATION {
            get {
                return ResourceManager.GetString("ERR_LOCATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No access to microphone.
        /// </summary>
        public static string ERR_MICROPHONE {
            get {
                return ResourceManager.GetString("ERR_MICROPHONE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No recorded audio file was found.
        /// </summary>
        public static string ERR_NOAUDIOFILE {
            get {
                return ResourceManager.GetString("ERR_NOAUDIOFILE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a record to continue!.
        /// </summary>
        public static string ERR_NODATA {
            get {
                return ResourceManager.GetString("ERR_NODATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This platform is not supported!.
        /// </summary>
        public static string ERR_NOSUPPORT {
            get {
                return ResourceManager.GetString("ERR_NOSUPPORT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading.
        /// </summary>
        public static string MES_LOAD {
            get {
                return ResourceManager.GetString("MES_LOAD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Buy the Pro version to change presets.
        /// </summary>
        public static string MES_PRO {
            get {
                return ResourceManager.GetString("MES_PRO", resourceCulture);
            }
        }
    }
}
