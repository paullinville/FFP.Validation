using FFP.CoreUtilities;
using System;
using System.Collections.Generic;

namespace FFP.Validations
{

    public class Rule<t> : IValidationRule
    {
        public Rule(ValidationHandler<t> handler, string ruleName, string description)
        {
            Handler = handler;
            RuleName = ruleName;
            Description = description;
        }

        public ValidationHandler<t> Handler { get; set; }
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

    public class ValidatedItemRule : IValidationRule //where t : IValidatedItem
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

        public ValidatedItemRule(string Validation, string Description, ValidationHandler<IValidatedItem> handler)
        {
            RuleName = Validation;
            this.Description = Description;
            ValidationHandler = handler;
        }

        bool IValidationRule.IsBroken(object itm)
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



        private ValidationHandler<IValidatedItem> ValidationHandler { get; set; }

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

        string IRule.RuleName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        string IRule.Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        ValidationSeverity IRule.Severity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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

    public class ValidateItemWrapper : List<IValidationRule>, IValidatedItem
    {
        public ValidateItemWrapper(object ruledItem)
        {
            RuledItem = ruledItem;
        }

        public Guid BOID { get; }

        private object RuledItem { get; set; }

        public void AddRule(IValidationRule Validation)
        {
            Add(Validation);
        }

        public string FriendlyName()
        {
            throw new NotImplementedException();
        }

        private BrokenValidationRules brokenRules { get; set; }


        public IEnumerable<IBrokenRule> InvalidRules(bool recheck = true)
        {
            if (recheck || brokenRules == null)
            {
                brokenRules = new BrokenValidationRules();
                List<IValidationRule> lst = new List<IValidationRule>();
                foreach (IValidationRule rle in this)
                {
                    if (rle.IsBroken(RuledItem))
                    {
                        brokenRules.Add(new BrokenValidationRule(rle, this));
                    }
                }
            }
            return brokenRules;
        }

    }
}

