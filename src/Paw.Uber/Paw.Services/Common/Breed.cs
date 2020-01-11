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
    public class Breed : IId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;
        
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _Type = 1;

        public decimal? AverageWeight
        {
            get { return _AverageWeight; }
            set { _AverageWeight = value; }
        }
        private decimal? _AverageWeight = null;

        [MaxLength(200)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = null;

        [MaxLength(200)]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private string _Country = String.Empty;

        [MaxLength(200)]
        public string Coat
        {
            get { return _Coat; }
            set { _Coat = value; }
        }
        private string _Coat = String.Empty;

        [MaxLength(200)]
        public string Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; }
        }
        private string _Pattern = String.Empty;
        
    }
}
