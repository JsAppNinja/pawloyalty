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
    public class ReferredBy
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = string.Empty;

        [MaxLength(200)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = string.Empty;

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [ForeignKey("ProviderGroupId")]
        public ProviderGroup ProviderGroup { get; set; }

        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = 0;

        public bool Archive
        {
            get { return _Archive; }
            set { _Archive = value; }
        }
        private bool _Archive = false;

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;

    }
}
