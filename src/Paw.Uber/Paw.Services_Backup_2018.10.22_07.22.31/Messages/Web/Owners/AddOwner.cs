using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Owners
{
    [Form(Title = "Add Owner")]
    public class AddOwner : IAdd<Owner>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [StartRow]
        [Width(6)]
        [Section(Text ="Owner")]
        [Display(Name = "First Name")]
        [MaxLength(100)]
        [Required]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;
        
        [Width(6)]
        [Display(Name = "Last Name")]
        [MaxLength(100)]
        [Required]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;

        [StartRow]
        [Width(6)]
        [Display(Name = "Primary Phone")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        [Required]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = String.Empty;

        [Width(6)]
        [Display(Name = "Alt Phone")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public string AltPhoneNumber
        {
            get { return _AltPhoneNumber; }
            set { _AltPhoneNumber = value; }
        }
        private string _AltPhoneNumber = String.Empty;

        [StartRow]
        [Width(6)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(300)]
        [Required]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = String.Empty;

        [StartRow]
        [Width(6)]
        [Section(Text = "Address")]
        [Display(Name = "Street Address")]
        [MaxLength(200)]
        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }
        private string _StreetAddress = String.Empty;
        
        [Width(6)]
        [Display(Name = "City")]
        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _City = String.Empty;

        [StartRow]
        [Width(6)]
        [Display(Name = "State")]
        [MaxLength(20)]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _State = String.Empty;
        
        [Width(6)]
        [Display(Name = "Postal Code")]
        [MaxLength(20)]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        private string _PostalCode = String.Empty;

        [StartRow]
        [Width(6)]
        [Section(Text = "Emergency Contact")]
        [Display(Name = "Contact Full Name")]
        [MaxLength(100)]
        public string EmergencyContactName
        {
            get { return _EmergencyContactName; }
            set { _EmergencyContactName = value; }
        }
        private string _EmergencyContactName = String.Empty;
        
        [Width(6)]
        [Display(Name = "Contact Phone")]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string EmergencyContactPhoneNumber
        {
            get { return _EmergencyContactPhoneNumber; }
            set { _EmergencyContactPhoneNumber = value; }
        }
        private string _EmergencyContactPhoneNumber = String.Empty;
        
        [ScaffoldColumn(false)]
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;
    }
}
