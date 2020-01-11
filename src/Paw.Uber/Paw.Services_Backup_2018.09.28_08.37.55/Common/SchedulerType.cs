using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class SchedulerType : IId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [MaxLength(200)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [MaxLength(50)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        private string _Action = String.Empty;

        [MaxLength(50)]
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        private string _Icon = String.Empty;



        public static readonly Guid Appointment = new Guid("{4F239E37-091F-4AEA-9AEE-0BB97B9649AD}");

        public static readonly Guid Block = new Guid("{669B5A7E-7D03-4D6A-AE88-CFB5E941CB14}");

        public static readonly Guid Resource = new Guid("{A8B91EA7-C041-4203-85D8-CD680335A267}"); // Room

    }
}
