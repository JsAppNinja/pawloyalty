using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paw.Web.Helpers
{
    public static class NavHelpers
    {
        public static MvcHtmlString ActiveOpen(this HtmlHelper htmlHelper, string expression)
        {
            if (htmlHelper.NavEval(expression))
            {
                return MvcHtmlString.Create("active open");
            }
            else
            {
                return MvcHtmlString.Create(string.Empty);
            }

        }

        public static bool NavEval(this HtmlHelper htmlHelper, string expressionList) // comma separated list of expressions without spaces
        {
            List<string> tokenList = expressionList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string token in tokenList)
            {
                List<string> expressionTokens = token.ExpressionTokens();

                if (expressionTokens[0] == "C")
                {
                    if (htmlHelper.MatchRouteValue("controller", expressionTokens[1]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (expressionTokens[0] == "A")
                {
                    if (htmlHelper.MatchRouteValue("action", expressionTokens[1]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (expressionTokens[0] == "R")
                {
                    if (htmlHelper.MatchRouteValue(expressionTokens[1], expressionTokens[2]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<string> ExpressionTokens(this string expression)
        {
            if (expression == null)
            {
                return new List<string>();
            }

            // Step 1. Split equals
            List<string> result = expression.Split(new string[] { "!", "=" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Step 2. return empty string if less than 2 tokens
            if (result.Count < 2)
            {
                return new List<string>();
            }

            return result;
        }

        public static bool TryGetRouteValue(this HtmlHelper htmlHelper, string key, out object value)
        {
            value = null;

            if (htmlHelper.ViewContext != null && htmlHelper.ViewContext.RouteData != null && htmlHelper.ViewContext.RouteData.Values.ContainsKey(key))
            {
                value = htmlHelper.ViewContext.RouteData.Values[key];
                return value != null;
            }

            return false;
        }

        public static bool MatchRouteValue(this HtmlHelper htmlHelper, string key, string value)
        {
            object existingValue = null;
            if (htmlHelper.TryGetRouteValue(key, out existingValue))
            {
                return existingValue.ToString().Equals(value);
            }

            return false;
        }
    }
}