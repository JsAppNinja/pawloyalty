using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Employee : IId, IProviderId
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

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
        
        public DateTime? TerminationDate
        {
            get { return _TerminationDate; }
            set { _TerminationDate = value; }
        }
        private DateTime? _TerminationDate = null;
        
        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        #region Test ...

        public bool? TestFlag
        {
            get { return _TestFlag; }
            set { _TestFlag = value; }
        }
        private bool? _TestFlag = null;

        public Guid? TestGroupId
        {
            get { return _TestGroupId; }
            set { _TestGroupId = value; }
        }
        private Guid? _TestGroupId = null;

        #endregion


        [NotMapped]
        public string DisplayColor
        {
            get { return _DisplayColor; }
            set { _DisplayColor = value; }
        }
        private string _DisplayColor = "#DDD";


    }
}
