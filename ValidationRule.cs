using FFP.CoreUtilities;

namespace FFP.Validations
{

    public class ValidationRule<t> : IValidationRule where t : IValidatedItem
    {
        public const string STR_EMPTY = "(Empty)";

        protected ValidationRule(string v)
        {
        }

        public ValidationRule(string Validation, string Description)
        {
            RuleName = Validation;
            this.Description = Description;
            this.Severity = ValidationSeverity.Critical;
        }

        public ValidationRule(string Validation, string Description, ValidationHandler<t> handler)
        {
            RuleName = Validation;
            this.Description = Description;
            ValidationHandler = handler;
        }

        bool IValidationRule.IsBroken(IValidatedItem itm)
        {

            return InvokedValidationBroken(itm);
        }

        private bool InvokedValidationBroken(IValidatedItem itm)
        {
            return InvokedValidationBroken(itm);
        }

        protected virtual bool InvokedValidationBroken(t itm)
        {
            if (ValidationHandler == null)
            {
                return false;
            }
            else
            {
                return ValidationHandler.Invoke(this, itm);
            }
        }

        private ValidationHandler<t> ValidationHandler { get; set; }

        public ValidationSeverity Severity { get; set; } = ValidationSeverity.Critical;

        public string RuleName { get; set; } = "";

        private string _description1;
        public string Description
        {
            get
            {
                return CSLA.Resources.Strings.GetResourceString(RuleName, _description1);
            }
            set
            {
                _description1 = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is IValidationRule)
            {
                IValidationRule ValidationToCompare = (IValidationRule)obj;
                if (ValidationToCompare.RuleName.CompareAbsolute(RuleName))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return Description;
        }

        public override int GetHashCode()
        {
            return (RuleName).GetHashCode();
        }


    }
}

