using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Paw.Services.Util
{
    public class CommonValueInjection : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            if (c.SourceProp.Name == c.TargetProp.Name && c.SourceProp.Type == c.TargetProp.Type || c.SourceProp.Type == Nullable.GetUnderlyingType(c.TargetProp.Type) || Nullable.GetUnderlyingType(c.SourceProp.Type) == c.TargetProp.Type)
            {
                return true;
            }

            return false;
        }


        public IPrincipal GetPrincipal()
        {
            return Thread.CurrentPrincipal;
        }

        protected override void Inject(object source, object target)
        {
            PropertyDescriptorCollection props = source.GetProps();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor item = props[i];
                PropertyDescriptor byName = target.GetProps().GetByName(item.Name);
                ConventionInfo conventionInfo = new ConventionInfo();
                conventionInfo.Source.Type = source.GetType();
                conventionInfo.SourceProp.Name = item.Name;
                conventionInfo.SourceProp.Value = item.GetValue(source);
                conventionInfo.SourceProp.Type = item.PropertyType;

                if (byName != null && (item.PropertyType == byName.PropertyType || item.PropertyType == Nullable.GetUnderlyingType(byName.PropertyType) || Nullable.GetUnderlyingType(item.PropertyType) == byName.PropertyType))
                {
                    conventionInfo.TargetProp.Name = byName.Name;
                    conventionInfo.TargetProp.Value = byName.GetValue(target);
                    conventionInfo.TargetProp.Type = byName.PropertyType;

                    if (this.Match(conventionInfo))
                    {
                        byName.SetValue(target, item.GetValue(source));
                    }
                }
            }
        }
    }
}
