using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{

    public class ValidationRule<t> : IRule
    {
        public ValidationRule(ValidationDelegate<t> handler, string ruleName, string description)
        {
            Handler = handler;
            RuleName = ruleName;
            Description = description;
        }

        public ValidationDelegate<t> Handler { get; set; }
        public string RuleName { get; set; }
        public string Description { get; set; }
        public ValidationSeverity Severity { get; set; } = ValidationSeverity.Critical;

        public bool IsBroken(object itm)
        {
            if (itm is t)
            {
                return Handler.Invoke(this, (t)itm);
            }
            return false;

        }
    }

    public class ValidatedItemRule : IRule //where t : IValidatedItem
    {
        public const string STR_EMPTY = "(Empty)";

        protected ValidatedItemRule(string v)
        {
        }

        public ValidatedItemRule(string Validation, string Description)
        {
            RuleName = Validation;
            this.Description = Description;
            this.Severity = ValidationSeverity.Critical;
        }

        public ValidatedItemRule(string Validation, string Description, ValidationDelegate<IValidatedItem> handler)
        {
            RuleName = Validation;
            this.Description = Description;
            ValidationHandler = handler;
        }

        bool IRule.IsBroken(object itm)
        {

            if (ValidationHandler == null)
            {
                return false;
            }
            else
            {
                if (itm is IValidatedItem)
                {
                    return ValidationHandler.Invoke(this, (IValidatedItem)itm);
                }
                else
                {
                    return ValidationHandler.Invoke(this, new ValidateItemWrapper(itm));
                }

            }
        }
        private ValidationDelegate<IValidatedItem> ValidationHandler { get; set; }

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

        string IRuleDescription.RuleName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        string IRuleDescription.Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        ValidationSeverity IRuleDescription.Severity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is IRule)
            {
                IRule ValidationToCompare = (IRule)obj;
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

