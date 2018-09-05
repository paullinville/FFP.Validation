using FFP.CoreUtilities;
using System;

namespace Validations
{
    public class BrokenRule : IBrokenRule
    {
        public string ObjectBroke { get; set; }
        public BrokenRule(IRuleDescription rle, object objectBroke)
        {
            Description = rle.Description;
            RuleName = rle.RuleName;
            Severity = rle.Severity;
            ObjectBroke = objectBroke.GetType().Name;
            Reason =  "Rule {0} with Severity {2} failed because {1}".FormatStr(rle.RuleName,  rle.Description, rle.Severity.EnumName());
        }

        public BrokenRule(string name, string desc, string propName, ValidationSeverity sev)
        {
            Description = desc + "";
            Reason = Description;
            RuleName = name + "";
            Severity = sev;
        }

        public BrokenRule(string name, string desc, string propName, ValidationSeverity sev, object objectBroke)
        {
            Description = desc + "";
            RuleName = name + "";
            ObjectBroke = objectBroke.GetType().Name; ;
            Severity = sev;
            Reason = Description;
        }

        public string Description { get; set; }

        public string RuleName { get; set; }

        public ValidationSeverity Severity { get; set; }

        public string Reason { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BrokenRule)
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
            return broke.RuleName + ObjectBroke + broke.Severity.EnumName();
        }
    }
}

