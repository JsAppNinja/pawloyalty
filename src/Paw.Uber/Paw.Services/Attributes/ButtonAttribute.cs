using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple =true)]
    public class ButtonAttribute : Attribute
    {
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _Text = String.Empty;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;


        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = 0;

        public string GetValue(ModelMetadata modelMetadata)
        {
            IId id = modelMetadata.Container as IId;

            if (id == null) return null;

            return id.Id.ToString();
        }

    }
}
