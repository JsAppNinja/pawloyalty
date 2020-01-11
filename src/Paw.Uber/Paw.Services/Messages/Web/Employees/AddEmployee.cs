using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Employees
{
    public class AddEmployee : IAdd<Employee>
    {
        [Key]
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

        [MaxLength(50)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;

        [MaxLength(50)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;

        [MaxLength(4)]
        public string Initials
        {
            get { return _Initials; }
            set { _Initials = value; }
        }
        private string _Initials = String.Empty;

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = string.Empty;

        //[DataType(DataType.PhoneNumber)]
        //public string PhoneNumber
        //{
        //    get { return _PhoneNumber; }
        //    set { _PhoneNumber = value; }
        //}
        //private string _PhoneNumber = string.Empty;

        public DateTime? TerminationDate
        {
            get { return _TerminationDate; }
            set { _TerminationDate = value; }
        }
        private DateTime? _TerminationDate = null;
    }
}
