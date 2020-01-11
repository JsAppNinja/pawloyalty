using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages;
using Paw.Services.Attributes;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace Paw.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AddViewDataActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            AddViewData(filterContext.Controller.ViewData.Model, filterContext.Controller);

            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
        }

        public static void AddViewData(object model, IController controller, int depth = 0)
        {
            if (depth > 3)
            {
                return;
            }

            // Step 1. Get the model
            if (model == null) return;

            // Step 2. Get the model type
            Type modelType = model.GetType();

            // Step 3. Get the IAddView Attributes
            List<IAddViewData> addViewDataList = AttributeHelper.GetAddViewDataList(modelType);

            // Step 4. Call IAddViewData.Add
            foreach (IAddViewData addViewData in addViewDataList)
            {
                addViewData.Add(model, controller);
            }

            /////////////////////////////////////////////////////

            List<IExecuteValue> executeValueList = AttributeHelper.GetAttributeList<IExecuteValue>(modelType).FindAll(x => x.Global && !string.IsNullOrEmpty(x.Key));

            foreach (IExecuteValue item in executeValueList)
            {
                ((ControllerBase)controller).ViewData[item.Key] = item.Execute(null, model, controller);
            }

        }
    }
}