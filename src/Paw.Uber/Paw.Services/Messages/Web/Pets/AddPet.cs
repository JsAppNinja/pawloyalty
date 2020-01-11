using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Breeds;
using Paw.Services.Messages.Web.Genders;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Pets
{
    public class AddPet : IAdd<Pet>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();


        [ScaffoldColumn(false)]
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [StartRow]
        [Width(12)]
        [Display(Name = "Owner")]
        [UIHint("ReadOnlyOwnerId")]
        [Required]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [StartRow]
        [Width(12)]
        [MaxLength(100)]
        [Required]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [StartRow]
        [Width(6)]
        [Display(Name = "Gender")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Method = "ExecuteList", Type = typeof(GetGenderList))]
        [Required]
        public Guid? GenderId
        {
            get { return _GenderId; }
            set { _GenderId = value; }
        }
        private Guid? _GenderId = null;
        
        [Width(6)]
        [Display(Name = "Breed")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Method = "ExecuteList", Type = typeof(GetBreedList))]
        public Guid? BreedId
        {
            get { return _BreedId; }
            set { _BreedId = value; }
        }
        private Guid? _BreedId = null;

        [StartRow]
        [Width(6)]
        [DataType(DataType.Date)]
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        private DateTime? _DOB = null;


        [Width(6)]
        public double? Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        private double? _Weight = null;

        //[Width(4)]
        //[Display(Name="Pet Class")]
        //public Guid? PetClassId
        //{
        //    get { return _PetClassId; }
        //    set { _PetClassId = value; }
        //}
        //private Guid? _PetClassId = null;

        [StartRow]
        [Width(12)]
        [Display(Name = "Rescue")]
        public bool IsRescue
        {
            get { return _IsRescue; }
            set { _IsRescue = value; }
        }
        private bool _IsRescue = false;

        [StartRow]
        [Width(12)]
        [Display(Name = "Blacklisted")]
        public bool Blacklisted
        {
            get { return _Blacklisted; }
            set { _Blacklisted = value; }
        }
        private bool _Blacklisted = false;

        [StartRow]
        [Width(12)]
        [Display(Name = "Deceased")]
        public bool IsDeceased
        {
            get { return _IsDeceased; }
            set { _IsDeceased = value; }
        }
        private bool _IsDeceased = false;

        //public Guid? VetId
        //{
        //    get { return _VetId; }
        //    set { _VetId = value; }
        //}
        //private Guid? _VetId = null;

        public int ExecuteNonQuery()
        {
            //CacheHelper.AddUpdateItem<ProviderGroup, Pet>(this.ProviderGroupId.ToString(), this, (cache) => cache.PetCollection.Where(x => x.Id == this.Id).SingleOrDefault());

            int result = MessageExtensions.ExecuteNonQuery<Pet>(this);
            return result;
        }
    }
}
