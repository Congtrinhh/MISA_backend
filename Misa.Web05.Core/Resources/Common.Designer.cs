﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Misa.Web05.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Common {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Common() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Misa.Web05.Core.Resources.Common", typeof(Common).Assembly);
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
        ///   Looks up a localized string similar to errors.
        /// </summary>
        public static string ErrorFieldName {
            get {
                return ResourceManager.GetString("ErrorFieldName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0.
        /// </summary>
        public static string PageIndexDefault {
            get {
                return ResourceManager.GetString("PageIndexDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 20.
        /// </summary>
        public static string PageSizeDefault {
            get {
                return ResourceManager.GetString("PageSizeDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$.
        /// </summary>
        public static string RegexEmail {
            get {
                return ResourceManager.GetString("RegexEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^NV-\d{4}$.
        /// </summary>
        public static string RegexEmployeeCode {
            get {
                return ResourceManager.GetString("RegexEmployeeCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$.
        /// </summary>
        public static string RegexPhoneNumber {
            get {
                return ResourceManager.GetString("RegexPhoneNumber", resourceCulture);
            }
        }
    }
}
