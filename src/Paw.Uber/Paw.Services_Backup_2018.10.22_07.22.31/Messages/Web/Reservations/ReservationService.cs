using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Reservations
{
    public class ReservationService
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Guid.Empty;

        [StartRow]
        [Display(Name = "Product")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetSkuList))]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [StartRow]
        [Display(Name = "Primary")]
        public bool IsPrimary
        {
            get { return _IsPrimary; }
            set { _IsPrimary = value; }
        }
        private bool _IsPrimary = true;
    }
}
