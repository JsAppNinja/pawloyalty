using Paw.Services.Attributes.ClientData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public static class ViewMetadataHelper
    {
        public static Dictionary<string, object> GetAttributes<T>(WebViewPage<T> webViewPage)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            if (webViewPage.ViewData.ModelMetadata.AdditionalValues.ContainsKey("attributes"))
            {
                attributes = webViewPage.ViewData.ModelMetadata.AdditionalValues["attributes"] as Dictionary<string, object>;
            }

            // AddDataAttributes
            AddAllAttributes(webViewPage, attributes);
                
            return attributes;
        }

        private static void AddAllAttributes<T>(WebViewPage<T> webViewPage, Dictionary<string, object> attributes)
        {
            if (webViewPage.ViewData.ModelMetadata.AdditionalValues.ContainsKey("AddDataAttributeList"))
            {
                // Data attributes
                List<AddDataAttributeAttribute> addDataAttributeList = webViewPage.ViewData.ModelMetadata.AdditionalValues["AddDataAttributeList"] as List<AddDataAttributeAttribute>;
                if (addDataAttributeList != null)
                {
                    foreach (var addDataAttribute in addDataAttributeList)
                    {
                        addDataAttribute.AddData(webViewPage.ViewData.ModelMetadata.Container, attributes);

                    }
                }
            }

            if (webViewPage.ViewData.ModelMetadata.AdditionalValues.ContainsKey("AddAttributeAttributeList"))
            {
                // Attributes
                List<AddAttributeAttribute> addAttributeAttributeList = webViewPage.ViewData.ModelMetadata.AdditionalValues["AddAttributeAttributeList"] as List<AddAttributeAttribute>;
                if (addAttributeAttributeList != null)
                {
                    foreach (var item in addAttributeAttributeList)
                    {
                        item.AddAttribute(webViewPage.ViewData.ModelMetadata.Container, attributes);
                    }
                }
            }
        }


        public static string GetAttributes(this Dictionary<string, object> attributes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var key in attributes.Keys)
            {
                stringBuilder.AppendFormat("{0}={1} ", key, attributes[key]);
            }

            return stringBuilder.ToString();
        }
    }
}
