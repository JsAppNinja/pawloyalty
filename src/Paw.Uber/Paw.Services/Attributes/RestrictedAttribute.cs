using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Principal;
using Paw.Services.Util;

namespace Paw.Services.Attributes
{
    public class RestrictedAttribute : Attribute
    {
        public RestrictedAttribute(string roles)
        {
            this.Roles = roles;
        }

        public string Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }
        private string _Roles = String.Empty;

        #region Runtime ...

        /// Runs in CustomDataAnnotationsModelMetadataProvider
        public static bool CreateMetadata(ModelMetadata modelMetadata, IEnumerable<Attribute> attributes)
        {
            RestrictedAttribute restrictedAttribute = attributes.OfType<RestrictedAttribute>().FirstOrDefault();
            if (restrictedAttribute != null)
            {
                modelMetadata.AdditionalValues.Add("Restricted", restrictedAttribute.Roles);
                return true;
            }

            return false;
        }

        /// Runs in control template
        public static bool AddReadOnly(IPrincipal principal, ViewDataDictionary viewDataDictionary)
        {
            if (viewDataDictionary.ModelMetadata.AdditionalValues.ContainsKey("Restricted") &&
                !viewDataDictionary.ModelMetadata.AdditionalValues["Restricted"].ToString().GetList().Any(x => principal.IsInRole(x)))
            {
                viewDataDictionary.ModelMetadata.AdditionalValues.Add("readonly", true);
                return true;
            }
            return false;
        }

        #endregion
    }
}
