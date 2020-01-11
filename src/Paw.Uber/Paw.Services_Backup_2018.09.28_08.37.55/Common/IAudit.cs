using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface IAudit
    {
        DateTime Updated { get; set; }

        Guid? UpdatedById { get; set; }

        DateTime Created { get; set; }

        Guid? CreatedById { get; set; }

        Guid MessageId { get; set; }

        [StringLength(1000)]
        string MessageType { get; set; }

        [StringLength(200)]
        string MachineName { get; set; }
    }
}
