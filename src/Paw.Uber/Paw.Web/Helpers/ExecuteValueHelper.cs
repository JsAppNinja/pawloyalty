using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Attributes;

namespace Paw.Web.Helpers
{
    public static class ExecuteValueHelper
    {
        public static object ExecuteValue(this HtmlHelper htmlHelper, string key = "")
        {
            List<IExecuteValue> executeValueList = new List<IExecuteValue>();

            if (!htmlHelper.ViewData.ModelMetadata.AdditionalValues.ContainsKey("ExecuteValueList"))
            {
                return null;
            }
            else
            {
                executeValueList = htmlHelper.ViewData.ModelMetadata.AdditionalValues["ExecuteValueList"] as List<IExecuteValue>;
            }

            if (executeValueList == null || executeValueList.Count < 1)
            {
                return null;
            }

            IExecuteValue executeValueItem = executeValueList.First();

            if (!string.IsNullOrEmpty(key))
            {
                executeValueItem = executeValueList.Find(x => x.Key == key);
                if (executeValueItem == null)
                {
                    return null;
                }
            }

            return executeValueItem.Execute(htmlHelper.ViewData.Model, htmlHelper.ViewData.ModelMetadata.Container, htmlHelper.ViewContext.Controller);
                
        }
    }
}