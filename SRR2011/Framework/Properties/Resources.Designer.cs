﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Disney.iDash.Framework.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Disney.iDash.Framework.Properties.Resources", typeof(Resources).Assembly);
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
        
        public static System.Drawing.Bitmap BackgroundUK {
            get {
                object obj = ResourceManager.GetObject("BackgroundUK", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        public static System.Drawing.Bitmap SignOnImage {
            get {
                object obj = ResourceManager.GetObject("SignOnImage", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM DSSRCTRL
        ///WHERE 
        ///   Username = &apos;&lt;UserName&gt;&apos;.
        /// </summary>
        public static string SQLCheckControlLog {
            get {
                return ResourceManager.GetString("SQLCheckControlLog", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE 
        ///	FROM DSSRLCK
        ///WHERE 
        ///	LOUSR=&apos;&lt;LOUSR&gt;&apos;.
        /// </summary>
        public static string SQLDeleteUserLocks1 {
            get {
                return ResourceManager.GetString("SQLDeleteUserLocks1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE 
        ///	FROM DSIALOK
        ///WHERE 
        ///	IAUSER=&apos;&lt;IAUSER&gt;&apos;.
        /// </summary>
        public static string SQLDeleteUserLocks2 {
            get {
                return ResourceManager.GetString("SQLDeleteUserLocks2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO DSSRCONTROL 
        ///     (USERNAME, PCNAME, SOFTWAREVERSION, LASTACTIVITY, ACTIVITYDESCRIPTION, ACTIVITYDETAIL)
        ///VALUES
        ///     (&apos;&lt;UserName&gt;&apos;, &apos;&lt;PCName&gt;&apos;, &apos;&lt;SoftwareVersion&gt;&apos;, &apos;&lt;LastActivity&gt;&apos;, &apos;&lt;ActivityDescription&gt;&apos;, &apos;&lt;ActivityDetail&gt;&apos;)      .
        /// </summary>
        public static string SQLInsertControlLog {
            get {
                return ResourceManager.GetString("SQLInsertControlLog", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE 
        ///   DSSRCONTROL 
        ///SET 
        ///   PCNAME = &apos;&lt;PCName&gt;&apos;,              
        ///   SoftwareVersion = &apos;&lt;SoftwareVersion&gt;&apos;,
        ///   LASTACTIVITY = &apos;&lt;LastActivity&gt;&apos;, 
        ///   ACTIVITYDESCRIPTION = &apos;&lt;ActivityDescription&gt;&apos;, 
        ///   ACTIVITYDETAIL = &apos;&lt;ActivityDetail&gt;&apos;
        ///WHERE 
        ///   UserName = &apos;&lt;UserName&gt;&apos;.
        /// </summary>
        public static string SQLUpdateControlLog {
            get {
                return ResourceManager.GetString("SQLUpdateControlLog", resourceCulture);
            }
        }
    }
}
