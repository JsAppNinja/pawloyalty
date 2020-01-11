using Paw.Services.Attributes;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Common
{
    public class Peep : INode<Peep>, IId, IParentId
    {
        [HiddenInput(DisplayValue=false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [UIHint("Peep")]
        public Peek Peek
        {
            get { return _Peek; }
            set { _Peek = value; }
        }
        private Peek _Peek = null;
        
        [ScaffoldColumn(false)]
        public Guid? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        private Guid? _ParentId = null;
        
        [Button(Text = "Add Child", Name ="addChild")]
        public ICollection<Peep> ChildCollection
        {
            get { return _ChildCollection; }
            set { _ChildCollection = value; }
        }
        private ICollection<Peep> _ChildCollection = new List<Peep>();

    }
}
