using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Util;

namespace Paw.Web.Helpers
{
    public class QuerystringInfo
    {
        public QuerystringInfo(HtmlHelper htmlHelper)
        {
            // Step 1 . Set HtmlHelper
            this.HtmlHelper = htmlHelper;
            if (htmlHelper == null && htmlHelper.ViewContext == null && htmlHelper.ViewContext.HttpContext == null)
            {
                return;
            }

            // Step 2. Add Querystrings
            foreach (string key in htmlHelper.ViewContext.HttpContext.Request.QueryString.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                if (!this.Dictionary.ContainsKey(key))
                {
                    this.Dictionary.Add(key, htmlHelper.ViewContext.HttpContext.Request.QueryString[key]);
                }
            }
        }

        private Dictionary<string, string> Dictionary
        {
            get { return _Dictionary; }
            set { _Dictionary = value; }
        }
        private Dictionary<string, string> _Dictionary = new Dictionary<string, string>();

        public HtmlHelper HtmlHelper
        {
            get { return _HtmlHelper; }
            set { _HtmlHelper = value; }
        }
        private HtmlHelper _HtmlHelper = null;

        public QuerystringInfo Set(string key, string value)
        {
            if (this.Dictionary.ContainsKey(key))
            {
                this.Dictionary[key] = value;
            }
            else
            {
                this.Dictionary.Add(key, value);
            }

            return this;
        }

        public QuerystringInfo Remove(string key)
        {
            if (this.Dictionary.ContainsKey(key))
            {
                this.Dictionary.Remove(key);
            }

            return this;
        }

        public MvcHtmlString GetQuerystring(bool include = true)
        {
            return new MvcHtmlString((include ? "?" : "") + this.Dictionary.Keys.AsString(x => string.Format("{0}={1}", x, this.Dictionary[x]), "&"));
        }

        public string GetValue(string key)
        {
            if (this.Dictionary.ContainsKey(key))
            {
                return this.Dictionary[key];
            }

            return null;
        }

        public DateTime? GetValueAsDate(string key)
        {
            string value = GetValue(key);
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        public Guid? GetValueAsGuid(string key)
        {
            string value = GetValue(key);
            Guid result;
            if (Guid.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        public int? GetValueAsInt32(string key)
        {
            string value = GetValue(key);
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }
    }
}