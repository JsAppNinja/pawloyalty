using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface ISchedule
    {
        bool IsAllDay { get; set; }

        DateTime? Start { get; set; }

        int DurationMin { get; set; }
        
    }
}
