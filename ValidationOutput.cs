using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{
    public class ValidationOutput
    {
        protected IRule Validation { get; set; }
        public ValidationOutput(IRule rl)
        {
            Validation = rl;
        }

        public virtual string Output()
        {
            return null;
            //System.Text.StringBuilder strBld = new System.Text.StringBuilder();
            //if (Validation.ItemValidated is IIdentifiable)
            //    strBld.Append(((IIdentifiable)Validation.ItemValidated).FriendlyName().Trim() + " " + Validation.ItemValidated.ToString());
            //else
            //    strBld.Append(Validation.ItemValidated.GetType().Name.Trim() + " " + Validation.ItemValidated.ToString());

            //strBld.Append(" Broken Validation with severity of ");
            //strBld.Append(Validation.Severity.EnumName());
            //if (!string.IsNullOrEmpty(Validation.PropertyName))
            //{
            //    strBld.Append(ControlChars.NewLine);
            //    strBld.Append("Property: ");
            //    strBld.Append(Validation.PropertyName.Trim() + ";");
            //    strBld.Append(" Current Value: ");
            //    if ((Validation.PropertyValue == null) || (Validation.PropertyValue is Guid && ((Guid)Validation.PropertyValue).IsEmpty()) || (Validation.PropertyValue is string && System.Convert.ToString(Validation.PropertyValue).IsEmpty()))
            //        strBld.Append(FFP.Validations.ValidationRule.STR_EMPTY);
            //    else
            //        strBld.Append(Validation.PropertyValue.ToString());
            //}
            //strBld.Append(ControlChars.NewLine);
            //strBld.Append("Description: ");
            //strBld.Append(Validation.Description);
            //return strBld.ToString();
        }
    }
}

