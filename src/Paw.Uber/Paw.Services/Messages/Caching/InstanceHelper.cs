using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace Paw.Services.Messages.Caching
{
    public static class InstanceHelper
    {
        public static void GetEndpointList(List<string> endpointList, TextWriter log)
        {
            foreach (string endpoint in endpointList)
            {
                GetAsync(endpoint, "").FireAndForget(log);
            }
        }

        public static async Task GetAsync(string endpoint, string affinityKey)
        {
            await endpoint
                .WithCookie("Affinity", affinityKey)
                .GetAsync();
        }

        public static async void FireAndForget(this Task task, TextWriter log = null)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                if (log != null)
                {
                    log.Write(e.Message);
                }
            }
        }
    }
}
