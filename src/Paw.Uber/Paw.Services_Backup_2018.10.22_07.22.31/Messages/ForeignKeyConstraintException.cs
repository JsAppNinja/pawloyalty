using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public class ForeignKeyConstraintException : Exception
    {
        public ForeignKeyConstraintException(string message, Exception innerException)
            : base(message, innerException)
        { 
            
        }
    }
}
