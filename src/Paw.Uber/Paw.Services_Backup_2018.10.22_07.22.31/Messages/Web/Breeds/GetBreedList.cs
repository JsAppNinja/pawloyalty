using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Breeds
{
    public class GetBreedList
    {
        public List<Breed> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.BreedSet.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
