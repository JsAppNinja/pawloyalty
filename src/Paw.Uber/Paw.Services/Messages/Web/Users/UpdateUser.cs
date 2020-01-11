using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Users
{
    public class UpdateUser : IUpdate<User>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [Display(Name="First Name")]
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
        
        [ReadOnly(true)]
        [Required]
        [MaxLength(254)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = String.Empty;

        #region Permission Set ...

        //[Section("User Permissions")]
        //[UIHint("DropDownList")]
        //[AddSelectList(DataTextField = "Key", DataValueField = "Value", Type = typeof(GetRoleList))]
        //public string Role
        //{
        //    get { return _Role; }
        //    set { _Role = value; }
        //}
        //private string _Role = RoleTypes.Intermediate;  

        //[Display(Name="Employee")]
        //[AddSelectList(DataTextField = "FullName", DataValueField = "Id", Type = typeof(GetEmployeeList))]
        //public Guid? EmployeeId
        //{
        //    get { return _EmployeeId; }
        //    set { _EmployeeId = value; }
        //}
        //private Guid? _EmployeeId = null;

        #endregion
    

    }
}
