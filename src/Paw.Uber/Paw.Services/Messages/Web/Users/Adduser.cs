using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Users
{
    public class AddUser : IAdd<User>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(40)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = string.Empty;

        [Required]
        [MaxLength(254)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = String.Empty;

        [ScaffoldColumn(false)]
        public Guid? ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid? _ProviderId = null;

        [ScaffoldColumn(false)]
        public Guid? ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid? _ProviderGroupId = null;

        public int Execute()
        {
            return MessageExtensions.ExecuteNonQuery(this); 
        }
    }
}
