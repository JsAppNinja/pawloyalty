using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paw.Web.Models
{
    public class ResourceViewModel
    {
        [DHXJson(Alias = "key")]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;


        [DHXJson(Alias = "room_number")]
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;


        [DHXJson(Alias = "label")]
        public string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        private string _ShortDescription = String.Empty;

    }
}