using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class AddReservation
    {
        public Guid Id // PrimarySkuLine
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Muid.Comb();

        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;

        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        public Guid ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid _ScheduleRuleId = Guid.Empty;

        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime _StartDate = DateTime.UtcNow.Date;
        
        public List<Guid> RelatedSkuIdList
        {
            get { return _RelatedSkuIdList; }
            set { _RelatedSkuIdList = value; }
        }
        private List<Guid> _RelatedSkuIdList = new List<Guid>();

        public int ExecuteNonQuery()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                // Step 1. Create the reservation
                Reservation reservation = new Reservation();
                reservation.Id = this.ReservationId;
                reservation.ProviderId = this.ProviderId;
                reservation.OwnerId = this.OwnerId;

                context.ReservationSet.Add(reservation);

                // Step 2. Get ScheduleRuleId
                ScheduleRule scheduleRule = context.ScheduleRuleSet.Where(x => x.Id == this.ScheduleRuleId && x.ProviderId == this.ProviderId).SingleOrDefault();
                if (scheduleRule == null)
                {
                    throw new InvalidOperationException("Invalid schedule rule.");
                }

                // Step 2. Get Sku
                Sku sku = context.SkuSet.Where(x => x.Id == this.SkuId && x.ProviderId == this.ProviderId).SingleOrDefault();
                if (sku == null)
                {
                    throw new InvalidOperationException("SkuId not found.");
                }

                // Step 3. Create skuLine
                SkuLine skuLine = new SkuLine();
                skuLine.Id = this.Id;
                skuLine.ProviderId = this.ProviderId;
                skuLine.PetId = this.PetId;
                skuLine.ReservationId = this.ReservationId;
                skuLine.StartDate = this.StartDate;
                skuLine.StartTime = this.StartDate.AddHours(scheduleRule.StartTime.Value.Hour).AddMinutes(scheduleRule.StartTime.Value.Minute);
                skuLine.ScheduleRuleId = this.ScheduleRuleId;

                // Memo
                skuLine.SkuId = sku.Id;
                skuLine.Name = sku.Name;
                skuLine.Description = sku.Description;
                skuLine.Amount = sku.Amount;

                context.SkuLineSet.Add(skuLine);

                //context.SaveChanges();

                // Step 4. Extras
                List<Sku> extraSkuList = context.SkuSet.Where(x => this.RelatedSkuIdList.Contains(x.Id) && x.ProviderId == this.ProviderId).ToList();

                foreach (Sku extraSku in extraSkuList)
                {
                    SkuLine extra = new SkuLine();
                    extra.ParentId = skuLine.Id;
                    extra.ProviderId = this.ProviderId;
                    extra.ReservationId = this.ReservationId;

                    // Memo
                    extra.SkuId = extraSku.Id;
                    extra.Name = extraSku.Name;
                    extra.Description = extraSku.Description;
                    extra.Amount = extraSku.Amount;

                    context.SkuLineSet.Add(extra);
                }

                // Step 5. Save 
                return context.SaveChanges();
            }
        }


    }
}
