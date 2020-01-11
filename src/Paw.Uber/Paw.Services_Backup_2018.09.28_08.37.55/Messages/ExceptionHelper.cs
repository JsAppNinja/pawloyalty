using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public static class ExceptionHelper
    {
        public static bool MatchException<T>(this Exception @this, Predicate<T> predicate) where T : Exception
        {
            T targetException = @this as T;

            if (targetException != null && predicate(targetException))
            {
                return true;
            }

            if (@this.InnerException != null)
            {
                bool result =  @this.InnerException.MatchException<T>(predicate);
                return result;
            }

            return false;
        }
    }
}
