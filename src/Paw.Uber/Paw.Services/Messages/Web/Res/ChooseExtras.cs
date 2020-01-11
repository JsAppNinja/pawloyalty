using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class ChooseExtras
    {
        [ScaffoldColumn(false)]
        public Guid Id // PrimarySkuLine
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
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        public List<Guid> ExtraSkuIdList
        {
            get { return _ExtraSkuIdList; }
            set { _ExtraSkuIdList = value; }
        }
        private List<Guid> _ExtraSkuIdList = new List<Guid>();

        public int ExecuteNonQuery()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                // Step 1. Get primary skuLine
                SkuLine primarySkuLine = context
                    .SkuLineSet
                    .Include("ChildCollection")
                    .Include("Reservation")
                    .Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();

                if (primarySkuLine == null)
                {
                    throw new InvalidOperationException("SkuLine not found.");
                }

                // Step 2. Add Skus
                foreach (Guid extraSkuId in this.ExtraSkuIdList)
                {
                    if (!primarySkuLine.ChildCollection.Any(x => x.SkuId == extraSkuId))
                    {
                        Sku extraSku = context.SkuSet.Where(x => x.Id == extraSkuId && x.ProviderId == this.ProviderId).SingleOrDefault();

                        context.SkuLineSet.Add(new SkuLine()
                        {
                            ProviderId = this.ProviderId,
                            Name = extraSku.Name,
                            Description = extraSku.Description,
                            Amount = extraSku.Amount,
                            ParentId = this.Id,
                            SkuId = extraSkuId
                        });
                    }
                }

                // Step 3. Drop Skus
                foreach (SkuLine extraSkuLine in primarySkuLine.ChildCollection)
                {
                    if (!this.ExtraSkuIdList.Any(x => x == extraSkuLine.SkuId))
                    {
                        extraSkuLine.Voided = DateTime.UtcNow;
                    }
                }

                return context.SaveChanges();

            }
        }
    }
}
