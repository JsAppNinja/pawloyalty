using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages;
using Paw.Services.Attributes;

namespace Paw.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NavAttribute : ActionFilterAttribute
    {
        public NavAttribute() { }

        public NavAttribute(string expresssion) { this.Expression = expresssion; }

        public string Expression
        {
            get { return _Expression; }
            set { _Expression = value; }
        }
        private string _Expression = String.Empty;


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            AddViewData(filterContext.Controller.ViewData.Model, filterContext);

            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public void AddViewData(object model, ResultExecutingContext filterContext)
        {
            // Step 1. Get nav token list
            List<string> navTokenList = new List<string>();

            // Step 2. Add new nav token list if necessary
            if (filterContext.Controller.ViewData.ContainsKey(NavTokenListKey))
            {
                navTokenList = filterContext.Controller.ViewData[NavTokenListKey] as List<string>;
            }
            else
            {
                filterContext.Controller.ViewData[NavTokenListKey] = navTokenList;
            }

            // Step 3. add expression
            navTokenList.Add(this.Expression);


        }

        public static readonly string NavTokenListKey = "NavTokenList";
    }
}