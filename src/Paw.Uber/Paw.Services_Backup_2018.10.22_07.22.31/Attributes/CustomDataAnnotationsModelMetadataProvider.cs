using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using Paw.Services.Attributes.ClientData;

namespace Paw.Services.Attributes
{
    public class CustomDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider 
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
                                                    Type containerType,
                                                    Func<object> modelAccessor,
                                                    Type modelType,
                                                    string propertyName)
        {

            ModelMetadata metadata = base.CreateMetadata(attributes,
                                                         containerType,
                                                         modelAccessor,
                                                         modelType,
                                                         propertyName);

            // Prefer [Display(Name="")] to [DisplayName]
            DisplayAttribute display = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (display != null)
            {
                string name = display.GetName();
                if (name != null)
                {
                    metadata.DisplayName = name;
                }

                // There was no 3.5 way to set these values
                metadata.Description = display.GetDescription();
                metadata.ShortDisplayName = display.GetShortName();
                metadata.Watermark = display.GetPrompt();
            }

            // Prefer [Editable] to [ReadOnly]
            EditableAttribute editable = attributes.OfType<EditableAttribute>().FirstOrDefault();
            if (editable != null)
            {
                metadata.IsReadOnly = !editable.AllowEdit;
            }

            // If [DisplayFormat(HtmlEncode=false)], set a data type name of "Html"
            // (if they didn't already set a data type)
            DisplayFormatAttribute displayFormat = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault();
            if (displayFormat != null
                    && !displayFormat.HtmlEncode
                    && String.IsNullOrWhiteSpace(metadata.DataTypeName))
            {
                metadata.DataTypeName = DataType.Html.ToString();
            }

            // Title Attribute
            TitleAttribute titleAttribute = attributes.OfType<TitleAttribute>().FirstOrDefault();
            if (titleAttribute != null)
            {
                // metadata.AdditionalValues.Add("Title", titleAttribute.Title);
                metadata.DisplayName = titleAttribute.Title;
            }

            // Section Attribute
            List<SectionAttribute> sectionAttributeList = attributes.OfType<SectionAttribute>().ToList();
            sectionAttributeList.Sort((x, y) => x.DisplayOrder.CompareTo(y.DisplayOrder));
            if (sectionAttributeList.Count > 0)
            {
                metadata.AdditionalValues.Add("SectionList", sectionAttributeList);
            }

            // Width
            WidthAttribute widthAttribute = attributes.OfType<WidthAttribute>().FirstOrDefault();
            if (widthAttribute != null)
            {
                metadata.AdditionalValues.Add("Width", widthAttribute.Columns);
            }

            // EndRow
            StartRowAttribute endRowAttribute = attributes.OfType<StartRowAttribute>().FirstOrDefault();
            if (endRowAttribute != null)
            {
                metadata.AdditionalValues.Add("StartRow", true);
            }

            // Button
            List<ButtonAttribute> buttonAttributeList = attributes.OfType<ButtonAttribute>().ToList();
            buttonAttributeList.Sort((x, y) => x.DisplayOrder.CompareTo(y.DisplayOrder));
            if (buttonAttributeList.Count > 0)
            {
                metadata.AdditionalValues.Add("ButtonList", buttonAttributeList);
            }

            // ClientData
            if (containerType != null)
            {
                PropertyInfo propertyInfo = containerType.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    List<AddDataAttributeAttribute> addDataAttributeList = propertyInfo.GetCustomAttributes<AddDataAttributeAttribute>().ToList();
                    if (addDataAttributeList.Count > 0)
                    {
                        metadata.AdditionalValues.Add("AddDataAttributeList", addDataAttributeList);
                    }

                    List<AddAttributeAttribute> addAttributeAttributeList = propertyInfo.GetCustomAttributes<AddAttributeAttribute>().ToList();
                    if (addAttributeAttributeList.Count > 0)
                    {
                        metadata.AdditionalValues.Add("AddAttributeAttributeList", addAttributeAttributeList);
                    }
                }
            }

            // restricted
            RestrictedAttribute.CreateMetadata(metadata, attributes);

            return metadata;
        }
    }
}