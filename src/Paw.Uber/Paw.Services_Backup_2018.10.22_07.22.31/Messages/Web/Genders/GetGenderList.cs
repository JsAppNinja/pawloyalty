using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Genders
{
    public class GetGenderList
    {
        public List<Gender> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {

                return context.GenderSet.OrderBy(x => x.DisplayOrder).ToList();

            }
        }
    }
}
