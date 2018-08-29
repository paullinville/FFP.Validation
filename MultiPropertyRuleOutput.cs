using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{
    public class MultiPropertyValidationOutput : ValidationOutput
    {

        public MultiPropertyValidationOutput() : base(null)
        {
        }


        public override string Output()
        {
            return null;
            //System.Text.StringBuilder strBld = new System.Text.StringBuilder();
            //if (Validation.ItemValidated is IValidatedItem)
            //    strBld.Append(((IValidatedItem)Validation.ItemValidated).FriendlyName().Trim());
            //else
            //    strBld.Append(Validation.ItemValidated.GetType().Name.Trim());

            //strBld.Append(" Broken Validation with severity of ");
            //strBld.Append(Validation.Severity.EnumName());
            //strBld.Append(ControlChars.NewLine);
            //foreach (string propName in ((MultiPropertyValidationRule)Validation).Properties)
            //{
            //    if (!string.IsNullOrEmpty(propName))
            //    {
            //        strBld.Append("Property: ");
            //        strBld.Append(propName.Trim() + ";");
            //        strBld.Append(" Current Value: ");

            //        if ((Validation.ItemValidated.PropertyValue(propName) == null) || (Validation.ItemValidated.PropertyValue(propName) is Guid && ((Guid)Validation.ItemValidated.PropertyValue(propName)).IsEmpty()) || (Validation.ItemValidated.PropertyValue(propName) is string && System.Convert.ToString(Validation.ItemValidated.PropertyValue(propName)).IsEmpty()))
            //            strBld.Append(FFP.Validations.ValidationRule.STR_EMPTY);
            //        else
            //            strBld.Append(Validation.ItemValidated.PropertyValue(propName).ToString());
            //    }
            //}

            //strBld.Append(ControlChars.NewLine);
            //strBld.Append("Description: ");
            //strBld.Append(Validation.Description);
            //strBld.Append(ControlChars.NewLine);
            //return strBld.ToString();
        }
    }
}

