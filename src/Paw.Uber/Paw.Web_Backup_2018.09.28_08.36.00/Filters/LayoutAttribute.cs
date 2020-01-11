using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claremont.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LayoutAttribute : ActionFilterAttribute
    {
        public LayoutAttribute(string layout)
        {
            this.Layout = layout;
        }
        
        public string Layout
        {
            get { return _Layout; }
            set { _Layout = value; }
        }
        private string _Layout = String.Empty;

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["CurrentLayout"] = this.Layout;
        }
    }
}