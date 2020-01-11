using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paw.Web.Models
{
    public static class ViewModelExtensions
    {
        public static List<ResourceViewModel> AsResourceViewModel(this IEnumerable<Resource> @this)
        {
            return @this.ToList().ConvertAll(x => new ResourceViewModel() { Id = x.Id, Key = x.Key, ShortDescription = x.ShortDescription });
        }

        public static List<BlockResourceViewModel> AsBlockViewModel(this IEnumerable<SchedulerEvent> @this)
        {
            return @this.ToList().ConvertAll(x => new BlockResourceViewModel() { Id = x.Id, Key = x.Resource.Key, ResourceId = x.ResourceId, Start = x.Start, End = x.End, Text = ""});
        }
    }
}