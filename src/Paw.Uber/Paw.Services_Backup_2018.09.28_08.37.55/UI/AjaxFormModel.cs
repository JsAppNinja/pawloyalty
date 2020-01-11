using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.UI
{
    public class AjaxFormModel
    {
            public string Id
            {
                get { return _Id; }
                set { _Id = value; }
            }
            private string _Id = "form0";

            public string Action
            {
                get { return _Action; }
                set { _Action = value; }
            }
            private string _Action = String.Empty;

            public bool AddAntiForgeryToken
            {
                get { return _AddAntiForgeryToken; }
                set { _AddAntiForgeryToken = value; }
            }
            private bool _AddAntiForgeryToken = false;

            public string SubmitLabel
            {
                get { return _SubmitLabel; }
                set { _SubmitLabel = value; }
            }
            private string _SubmitLabel = "Save";

            public string FormTitle
            {
                get { return _FormTitle; }
                set { _FormTitle = value; }
            }
            private string _FormTitle = String.Empty;

            public string HttpMethod
            {
                get { return _HttpMethod; }
                set { _HttpMethod = value; }
            }
            private string _HttpMethod = "GET";

            public string OnSuccess
            {
                get { return _OnSuccess; }
                set { _OnSuccess = value; }
            }
            private string _OnSuccess = String.Empty;
        
    }
}
