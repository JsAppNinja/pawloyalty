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
using Hangfire.Dashboard;

namespace Paw.Web.Filters
{
    public class HangfireDashboardFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}