using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Payments
{
    public class AddCCPayment
    {
        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Guid.Empty;

        public string CCToken
        {
            get { return _CCToken; }
            set { _CCToken = value; }
        }
        private string _CCToken = String.Empty;
        
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private decimal? _Amount = null;

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

    }
}
