﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookmarkletBuilder.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BookmarkletMain.js")]
        public string BookmarkletMainJSWrapper {
            get {
                return ((string)(this["BookmarkletMainJSWrapper"]));
            }
            set {
                this["BookmarkletMainJSWrapper"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Bookmarklets.html")]
        public string BookmarkletHtmlOutput {
            get {
                return ((string)(this["BookmarkletHtmlOutput"]));
            }
            set {
                this["BookmarkletHtmlOutput"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Bookmarklets")]
        public string BookmarkletsFolder {
            get {
                return ((string)(this["BookmarkletsFolder"]));
            }
            set {
                this["BookmarkletsFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CRM Bookmarklets.template")]
        public string BookmarkletHtmlTemplate {
            get {
                return ((string)(this["BookmarkletHtmlTemplate"]));
            }
            set {
                this["BookmarkletHtmlTemplate"] = value;
            }
        }
    }
}
