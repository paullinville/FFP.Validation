using CSLA;
using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;
using System.Collections.Generic;

namespace FFP.Validations
{
    public class BrokenValidationRule : IBrokenRule
    {
        private Guid IValidationdItemID { get; set; }
        public BrokenValidationRule(IValidationRule rle, IIdentifiable objectBroke)
        {
            IsBroken = true;
            {
                var withBlock = rle;
                Description = withBlock.Description;
                RuleName = withBlock.RuleName;
                Severity = withBlock.Severity;
                IValidationdItemID = objectBroke.IfNullBOID();
            }
 
        }

        public BrokenValidationRule(string name, string desc, string propName, ValidationSeverity sev)
        {
            IsBroken = true;
            Description = desc + "";
            PropertyName = propName + "";
            RuleName = name + "";
            Severity = sev;
        }

        public BrokenValidationRule(IIdentifiable objectBroke, string name, string desc, string propName, ValidationSeverity sev)
        {
            IsBroken = true;
            Description = desc + "";
            PropertyName = propName + "";
            RuleName = name + "";
            IValidationdItemID = objectBroke.IfNullBOID();
            Severity = sev;
        }

        public string Description { get; set; }

        private List<string> lvProperties;
        public bool HandlesProperty(string propName)
        {
            if (lvProperties != null)
                return PropertyName.CompareAbsolute(propName) || lvProperties.ContainsAbsolute(propName);
            else
                return PropertyName.CompareAbsolute(propName);
        }

        public bool IsBroken { get; set; }

        public string PropertyName { get; set; }

        public object PropertyValue { get; set; }

        public string RuleName { get; set; }

        public ValidationSeverity Severity { get; set; }

        public IValidatedItem ItemValidated { get; set; }
        public string Reason { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

