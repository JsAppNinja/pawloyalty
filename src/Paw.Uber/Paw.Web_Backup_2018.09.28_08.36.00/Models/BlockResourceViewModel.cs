using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paw.Web.Models
{
    public class BlockResourceViewModel
    {
        [DHXJson(Alias = "id")]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [DHXJson(Alias = "text")]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _Text = String.Empty;
        
        [DHXJson(Alias = "start_date")]
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;
        
        [DHXJson(Alias = "end_date")]
        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;

        [DHXJson(Alias = "resource_id")]
        public Guid? ResourceId
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
        private Guid? _ResourceId = null;
        
        [DHXJson(Alias = "room_number")]
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        public string OwnerName
        {
            get { return _OwnerName; }
            set { _OwnerName = value; }
        }
        private string _OwnerName = String.Empty;

        public string PetName
        {
            get { return _PetName; }
            set { _PetName = value; }
        }
        private string _PetName = String.Empty;




    }
}