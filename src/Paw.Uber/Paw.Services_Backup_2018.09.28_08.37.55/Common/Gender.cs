using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Gender : IId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = string.Empty;

        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = 0;


        public static readonly Guid Male = new Guid("{1957032F-5668-4B23-8B96-A93060D0F580}");

        public static readonly Guid Female = new Guid("{6EAC744B-A89E-4DD9-8ECD-09759DA840AE}");

        public static readonly Guid MaleNeutered = new Guid("{500EBE4C-F9E1-41AE-A055-C7B2957C15B8}");

        public static readonly Guid FemaleSpayed = new Guid("{7486989E-B831-4684-88DC-1E92ADDEDABE}");


    }
}
