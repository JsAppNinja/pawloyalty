using Paw.Services.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Helpers
{
    public static class ControllerAjaxExtensions
    {
        public static void AddAjaxPost(this Controller controller, string url, string title, Action<AjaxFormModel> config = null)
        {
            AjaxFormModel ajaxFormModel = new AjaxFormModel() { Action = url, FormTitle = title, HttpMethod = "POST" };

            if (config != null)
            {
                config(ajaxFormModel);
            }

            controller.ViewData["AjaxFormModel"] = ajaxFormModel;
        }

        public static void AddAjaxGet(this Controller controller, string url, string title, Action<AjaxFormModel> config = null)
        {
            AjaxFormModel ajaxFormModel = new AjaxFormModel() { Action = url, FormTitle = title, HttpMethod = "POST" };

            if (config != null)
            {
                config(ajaxFormModel);
            }

            controller.ViewData["AjaxFormModel"] = ajaxFormModel;
        }
    }
}