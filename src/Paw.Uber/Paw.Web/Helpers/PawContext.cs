using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Helpers
{
    public class PawContext
    {
        public Provider Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }
        private Provider _Provider = null;

        public static PawContext Create(HtmlHelper htmlHelper)
        {
            PawContext context = new PawContext();

            // Step 1. Get the provider
            context.Provider =  htmlHelper.ViewData["__Provider"] as Provider;

            return context;
        }


    }
}