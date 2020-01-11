using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Views.Shared
{
    public class FilterModel
    {
        public List<SelectListItem> SelectListItemList
        {
            get { return _SelectListItemList; }
            set { _SelectListItemList = value; }
        }
        private List<SelectListItem> _SelectListItemList = new List<SelectListItem>();

        public string SelectAllCaption
        {
            get { return _SelectAllCaption; }
            set { _SelectAllCaption = value; }
        }
        private string _SelectAllCaption = String.Empty;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _Id = String.Empty;


    }
}