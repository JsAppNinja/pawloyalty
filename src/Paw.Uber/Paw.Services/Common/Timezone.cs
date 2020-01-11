using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Timezone : IId
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
        private string _Name = String.Empty;

        [MaxLength(100)]
        public string TZString
        {
            get { return _TZString; }
            set { _TZString = value; }
        }
        private string _TZString = String.Empty; // https://en.wikipedia.org/wiki/List_of_tz_database_time_zones

        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = int.MaxValue;


        public static readonly Guid Pacific = new Guid("{02A00A2B-F32C-4655-BF04-6E2DD745DED1}");
        public static readonly Guid Arizona = new Guid("{2B552FA9-322A-4047-ADB1-51DACC624DE3}");
        public static readonly Guid Mountain = new Guid("{C2C69B62-8FC1-47BE-8DC5-50A59CD3CFBE}");
        public static readonly Guid Central = new Guid("{F1CB658B-11E4-4B9D-B96F-5D57C327AE16}");
        public static readonly Guid Eastern = new Guid("{C310ACF3-D5FB-497B-B298-761F7FFF31D4}");

    }
}
