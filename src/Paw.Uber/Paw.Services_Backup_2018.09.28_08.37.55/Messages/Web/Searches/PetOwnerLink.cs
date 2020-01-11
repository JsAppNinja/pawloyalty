using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class PetOwnerLink
    {
        public Guid? Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid? _Id = null;


        public string Pet
        {
            get { return _Pet; }
            set { _Pet = value; }
        }
        private string _Pet = String.Empty;

        public string Breed
        {
            get { return _Breed; }
            set { _Breed = value; }
        }
        private string _Breed = "Unknown";
        
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

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

        public string PetAndOwner
        {
            get { return this.ToString(); }
        }
        public string LastFirst
        {
            get { return this.LastName + ", " + this.FirstName; }
        }

        public string Fullname
        {
            get { return this.FirstName + " " + this.LastName; }
            set { }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", this.Pet, this.Breed, this.Fullname);
        }

    }
}
