﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMS.Infrastructure.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class GlobalResource {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GlobalResource() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("IMS.Infrastructure.Resources.GlobalResource", typeof(GlobalResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string RecordNotFound {
            get {
                return ResourceManager.GetString("RecordNotFound", resourceCulture);
            }
        }
        
        internal static string NoValidAction {
            get {
                return ResourceManager.GetString("NoValidAction", resourceCulture);
            }
        }
        
        internal static string EmailOrPasswordNotValid {
            get {
                return ResourceManager.GetString("EmailOrPasswordNotValid", resourceCulture);
            }
        }
        
        internal static string ResetPasswordExpired {
            get {
                return ResourceManager.GetString("ResetPasswordExpired", resourceCulture);
            }
        }
        
        internal static string RequestResetPassword {
            get {
                return ResourceManager.GetString("RequestResetPassword", resourceCulture);
            }
        }
        
        internal static string NotAuthenticated {
            get {
                return ResourceManager.GetString("NotAuthenticated", resourceCulture);
            }
        }
        
        internal static string NotAuthorized {
            get {
                return ResourceManager.GetString("NotAuthorized", resourceCulture);
            }
        }
    }
}
