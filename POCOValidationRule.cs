namespace FFP.Validations
{
    public abstract class POCOValidationRuleBase<t> : ITypeRule<t>
    {
        protected POCOValidationRuleBase(string ruleName, 
                                         string description)
        {
            RuleName = ruleName;
            Description = description;
            Severity = ValidationSeverity.Critical;
        }

        public string RuleName { get; set; }
        public string Description { get; set; }
        public ValidationSeverity Severity { get; set; } = ValidationSeverity.Critical;

        public abstract bool IsBroken(t itm);

        bool IValidationRule.IsBroken(object itm)
        {
            if (itm is t)
                return IsBroken((t)itm);
            return false;
        }
    }

}

