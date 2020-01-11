using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class FormAttribute : Attribute, IAddViewData
    {
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = string.Empty;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string _Message = string.Empty;

        public string CancelUrl
        {
            get { return _CancelUrl; }
            set { _CancelUrl = value; }
        }
        private string _CancelUrl = ".";

        public bool AddAntiForgeryToken
        {
            get { return _AddAntiForgeryToken; }
            set { _AddAntiForgeryToken = value; }
        }
        private bool _AddAntiForgeryToken = false; 

        public void Add(IController controller)
        {
            // Step 1. Add From Info
            ((ControllerBase)controller).ViewData[FormTitleKey] = this.Title;
            ((ControllerBase)controller).ViewData[FormMessageKey] = this.Message;
            ((ControllerBase)controller).ViewData[FormCancelUrlKey] = this.CancelUrl;

            if (this.AddAntiForgeryToken)
            {
                ((ControllerBase)controller).ViewData[AddAntiForgeryTokenKey] = this.AddAntiForgeryToken;
            }
        }

        public static string FormTitleKey = "FormTitle";

        public static string FormMessageKey = "FormMessage";

        public static string FormCancelUrlKey = "FormCancelUrl";

        public static string AddAntiForgeryTokenKey = "AddAntiForgeryToken";
    }
}
