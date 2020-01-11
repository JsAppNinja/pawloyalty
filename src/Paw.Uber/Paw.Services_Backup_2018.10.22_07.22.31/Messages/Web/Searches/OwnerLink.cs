using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class OwnerLink
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;
        
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

        public int Score
        {
            get { return _Score; }
            set { _Score = value; }
        }
        private int _Score = 0;



    }
}
