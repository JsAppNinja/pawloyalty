using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Helpers
{
    public static class SchedulerHelpers
    {
        public static MvcHtmlString DateView(this HtmlHelper htmlHelper, string id, DateTime date, int start, int end, List<ColumnInfo> columnInfoList)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<table id='{id}' class='table-bordered'>");

            // Step 1. Date
            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append($"<td class='column-date text-center' colspan='{columnInfoList.Count}'>{date:ddd d/M}</td>");
            sb.Append("</tr>");

            // Step 2. Col headings
            sb.Append("<tr>");
            sb.Append("<td></td>");
            foreach (ColumnInfo columnInfo in columnInfoList)
            {
                sb.Append($"<td class='column-display column-header text-center'>{columnInfo.Title}</td>");
            }
            sb.Append("</tr>");

            // Step 3. 
            for (int i = start; i < end; i++)
            {
                DateTime rowTime = new DateTime(date.Year, date.Month, date.Day, i, 0, 0);

                // Top of the hour
                sb.Append("<tr class=''>");
                sb.Append($"<td class='text-right hour-display'><span class='hour'>{rowTime:h:mm tt}</span></td>");

                foreach (ColumnInfo columnInfo in columnInfoList)
                {
                    sb.Append($"<td id='{GetCellId(id, columnInfo.Id, rowTime)}' class='column-display'></td>");
                }

                sb.Append("</tr>");

                rowTime = rowTime.AddMinutes(30);

                sb.Append("<tr class=''>");
                sb.Append($"<td class='text-right hour-display-alt'><span class='hour-alt' >{rowTime:h:mm tt}</span></td>");

                foreach (ColumnInfo columnInfo in columnInfoList)
                {
                    sb.Append($"<td id='{GetCellId(id, columnInfo.Id, rowTime)}' class='column-display-alt'></td>");
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return new MvcHtmlString(sb.ToString());

        }

        private static string GetCellId(string id, string columnId, DateTime dateTime)
        {
            return $"{id}-{columnId}-{dateTime:YYMMddHHmm}";
        }
    }

    public class ColumnInfo
    {

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _Id = string.Empty;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = string.Empty;


    }
}