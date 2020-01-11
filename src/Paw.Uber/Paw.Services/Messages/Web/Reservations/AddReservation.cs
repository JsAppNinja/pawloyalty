using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using System.ComponentModel.DataAnnotations;
using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Messages.Web.Pets;

namespace Paw.Services.Messages.Web.Reservations
{
    public class AddReservation : IAdd<Reservation>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public List<Guid> PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private List<Guid> _PetId = new List<Guid>();

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [DataType(DataType.Date)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime? _StartDate = null;

        [DataType(DataType.Date)]
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private DateTime? _EndDate = null;

        [DataType(DataType.Time)]
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        private DateTime? _StartTime = null;

        [DataType(DataType.Time)]
        public DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        private DateTime? _EndTime = null;

        public Guid? PrimarySkuId
        {
            get { return _PrimarySkuId; }
            set { _PrimarySkuId = value; }
        }
        private Guid? _PrimarySkuId = null;

        [ScaffoldColumn(false)]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [MaxLength(400)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        private string _Note = string.Empty;
    }


}
