using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class GetReservationInfo
    {
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty ;

        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;





    }
}
