using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class DeleteScheduleRule : IDeleteProvider<ScheduleRule>
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

        public int ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.Database.ExecuteSqlCommand(
                    @"UPDATE [ScheduleBlock] SET ScheduleRuleId = NULL WHERE ProviderId = @ProviderId AND ScheduleRuleId = @Id 
                    DELETE [ScheduleRule] WHERE Id = @Id AND ProviderId = @ProviderId", 
                    new SqlParameter("@Id", this.Id), new SqlParameter("@ProviderId", this.ProviderId));
            }
        }
    }
}
