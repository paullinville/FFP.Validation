namespace FFP.Validations
{
    public abstract class TypedRuleBase<t> : ITypeRule<t>
    {
        protected TypedRuleBase(string ruleName, 
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

        bool IRule.IsBroken(object itm)
        {
            if (itm is t)
                return IsBroken((t)itm);
            return false;
        }
    }

}

