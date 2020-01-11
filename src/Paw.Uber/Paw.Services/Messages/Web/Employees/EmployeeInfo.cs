using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Employees
{
    // Used for resource view on schduler
    public class EmployeeInfo
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;
        
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;
        
        public string Initials
        {
            get { return _Initials; }
            set { _Initials = value; }
        }
        private string _Initials = String.Empty;

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        private string _Color = "#DDD";

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
