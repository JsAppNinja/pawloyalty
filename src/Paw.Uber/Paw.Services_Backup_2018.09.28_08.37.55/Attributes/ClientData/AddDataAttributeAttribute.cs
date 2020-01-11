using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes.ClientData
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AddDataAttributeAttribute : Attribute
    {
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        // Sets value early
        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }
        private string _PropertyName = String.Empty;

        // Evaluates at runtime
        public string Expression
        {
            get { return _Expression; }
            set { _Expression = value; }
        }
        private string _Expression = String.Empty;

        // Static
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _Value = String.Empty;

        public virtual void AddData(object model, Dictionary<string, object> attributes)
        {
            // Set name if only property name is present
            if (!string.IsNullOrEmpty(this.PropertyName) && string.IsNullOrEmpty(this.Name))
            {
                this.Name = this.PropertyName;
            }

            PropertyInfo propertyInfo = model.GetType().GetProperty(this.PropertyName);

            if (propertyInfo != null && model != null) // Get property
            {
                attributes.Add($"data-{this.Name}", propertyInfo.GetValue(model));
            }
            else if (!string.IsNullOrWhiteSpace(this.Expression)) // Set expression
            {
                attributes.Add($"data-{this.Name}", "$" + this.Expression);
            }
            else if (!string.IsNullOrEmpty(this.Value))
            {
                attributes.Add($"data-{this.Name}", this.Value);
            }
        }

        public static void AddData<T>(WebViewPage<T> webViewPage, Dictionary<string, object> attributes)
        {
            if (!webViewPage.ViewData.ModelMetadata.AdditionalValues.ContainsKey("AddDataAttributeList")) return;

            List<AddDataAttributeAttribute> addDataAttributeList = webViewPage.ViewData.ModelMetadata.AdditionalValues["AddDataAttributeList"] as List<AddDataAttributeAttribute>;
            if (addDataAttributeList != null)
            {
                foreach (var addDataAttribute in addDataAttributeList)
                {
                    addDataAttribute.AddData(webViewPage.ViewData.ModelMetadata.Container, attributes);

                }
            }
        }
    }
}
