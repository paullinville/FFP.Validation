namespace Validations
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

}

