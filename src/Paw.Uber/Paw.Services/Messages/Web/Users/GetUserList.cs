using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Users
{
    public class GetUserList
    {

        public List<User> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                List<User> result = context.UserSet.ToList();
                result.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                return result;
            }
        }
    }
}
