﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.7.0.0")]
    internal sealed partial class UnitSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static UnitSettings defaultInstance = ((UnitSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new UnitSettings())));
        
        public static UnitSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int RoundingAccuracy {
            get {
                return ((int)(this["RoundingAccuracy"]));
            }
            set {
                this["RoundingAccuracy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool RoundOnUnitChange {
            get {
                return ((bool)(this["RoundOnUnitChange"]));
            }
            set {
                this["RoundOnUnitChange"] = value;
            }
        }
    }
}
