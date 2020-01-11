using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using Paw.Services.Common;
using System.Security.Principal;
using System.Threading;

namespace Paw.Services.Util
{
    public class ExcludeReadOnly : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            if (c.SourceProp.Name == c.TargetProp.Name)
            {
                if (IsReadOnly(c))
                {
                    return false;
                }
                

                return true;
            }

            return false;
        }

        protected bool IsReadOnly(ConventionInfo c)
        {
            var readOnly = c.Source.Type.GetProperty(c.SourceProp.Name).GetCustomAttribute<ReadOnlyAttribute>();
            if (readOnly != null && readOnly.IsReadOnly)
                return true;

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

                if (byName != null && (item.PropertyType == byName.PropertyType || (byName.PropertyType.IsGenericType && item.PropertyType ==  Nullable.GetUnderlyingType(byName.PropertyType))))
                {
                    conventionInfo.TargetProp.Name = byName.Name;
                    conventionInfo.TargetProp.Value = byName.GetValue(target);
                    conventionInfo.TargetProp.Type = byName.PropertyType;

                    if (this.Match(conventionInfo))
                    {
                        if (byName.IsReadOnly) // We let the convention dictate what get's set
                        {
                            target.GetType().GetProperty(item.Name).SetValue(target, item.GetValue(source));
                        }
                        else
                        {
                            byName.SetValue(target, item.GetValue(source));
                        }
                    }
                }
            }
        }
    }
}
