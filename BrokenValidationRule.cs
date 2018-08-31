using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{
    public class BrokenValidationRule : IBrokenRule
    {
        public object ObjectBroke { get; set; }
        public BrokenValidationRule(IRule rle, object objectBroke)
        {
            Description = rle.Description;
            RuleName = rle.RuleName;
            Severity = rle.Severity;
            ObjectBroke = objectBroke;
            Reason = rle.Description;
        }

        public BrokenValidationRule(string name, string desc, string propName, ValidationSeverity sev)
        {
            Description = desc + "";
            Reason = Description;
            RuleName = name + "";
            Severity = sev;
        }

        public BrokenValidationRule(string name, string desc, string propName, ValidationSeverity sev, object objectBroke)
        {
            Description = desc + "";
            RuleName = name + "";
            ObjectBroke = objectBroke;
            Severity = sev;
            Reason = Description;
        }

        public string Description { get; set; }

        public string RuleName { get; set; }

        public ValidationSeverity Severity { get; set; }

        public string Reason { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BrokenValidationRule)
            {
                IBrokenRule OTHER = (IBrokenRule)obj;
                return identifier(this) == identifier(OTHER);
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return identifier(this).GetHashCode();
        }

        private string identifier(IBrokenRule broke)
        {
            return broke.RuleName + broke.Severity.EnumName();
        }
    }
}

