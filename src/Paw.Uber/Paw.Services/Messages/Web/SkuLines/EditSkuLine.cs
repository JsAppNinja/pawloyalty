using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.SkuLines
{
    public class EditSkuLine
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [StartRow]
        [Display(Name = "Service")]
        [Width(12)]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "NameAndPrice", DataValueField = "Id", Type = typeof(GetPrimaryOrExtraSkuList))]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [ScaffoldColumn(false)]
        public List<EditSkuLine> ChildList
        {
            get { return _ChildList; }
            set { _ChildList = value; }
        }
        private List<EditSkuLine> _ChildList = new List<EditSkuLine>();

        #region Display ...

        [ScaffoldColumn(false)]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string _Message = String.Empty;

        #endregion

        #region Filter ...

        [ScaffoldColumn(false)]
        public Guid? ParentSkuId
        {
            get { return _ParentSkuId; }
            set { _ParentSkuId = value; }
        }
        private Guid? _ParentSkuId = null;


        [ScaffoldColumn(false)]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        #endregion

        [ScaffoldColumn(false)]
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        private bool _IsNew = true;


    }
}
