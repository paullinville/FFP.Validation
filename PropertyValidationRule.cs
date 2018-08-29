using FFP.CoreUtilities;
using System;
using System.Reflection;

namespace FFP.Validations


{
    public class PropertyValidationRule<t, u> : IPropertyValidationRule<t, u>
    {

        public PropertyValidationRule(string ruleName, string Description, String propname, PropValidationHandler<t, u> handler) : this(ruleName, Description, propname)
        {
            PropertyName = propname;
            Handler = handler;
        }

        public PropertyValidationRule(string ruleName, string Description, String propname)
        {
            this.RuleName = RuleName;
            PropertyName = propname;
            this.Description = Description;
        }
        public PropertyInfo PropInfo { get; set; }
        private PropValidationHandler<t, u> Handler { get; set; }

        protected bool InvokedValidationBroken(object itm)
        {
            if (Handler == null)
            {
                return false;
            }
            else
            {
                return Handler.Invoke(this, itm);
            }
        }

        public string PropertyName { get; set; }
        public virtual u AllowedValue { get; set; }
        public string RuleName { get; set; }
        public string Description { get; set; }
        public ValidationSeverity Severity { get; set; }

        public t PropertyValue(object ItemValidated)
        {

            if(PropInfo != null)
            {
                return (t)PropInfo.GetMethod.Invoke(ItemValidated, null);
            }
            else if (ItemValidated != null && PropertyName.IsNotNullOrBlank())
            {
                PropertyInfo propInfo = ItemValidated.GetType().GetProperty(PropertyName);

                // Use the instance to call the method without arguments
                ;
                return (t)propInfo.GetMethod.Invoke(ItemValidated, null);
            }
            else
            {
                return default(t);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is IPropertyValidationRule<t, u>)
            {
                IPropertyValidationRule<t, u> ValidationToCompare = (IPropertyValidationRule<t, u>)obj;
                if (ValidationToCompare.RuleName.CompareAbsolute(RuleName) && ValidationToCompare.PropertyName.CompareAbsolute(PropertyName))
                {

                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (RuleName + PropertyName).GetHashCode();
        }

        public bool IsBroken(IValidatedItem itm)
        {
            return InvokedValidationBroken(itm);
        }
    }
}

