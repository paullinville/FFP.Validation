using FFP.CoreUtilities;
using System;
using System.Collections.Generic;

namespace FFP.Validations
{
    [Serializable()]
    public class MultiPropertyValidationRule : ValidationRule
    {
        public MultiPropertyValidationRule(string Validation, string Description) : base(Validation, Description)
        {
        }

        public MultiPropertyValidationRule(string Validation, string Description, string Property) : base(Validation, Description, Property)
        {
        }

        public MultiPropertyValidationRule(string Validation, string Description, string Property, ValidationHandler handler) : base(Validation, Description, Property, handler)
        {
        }

        private List<string> lvProperties = new List<string>();
        public List<string> Properties
        {
            get
            {
                return lvProperties;
            }
        }



        public override object AllowedValue
        {
            get
            {
                return base.AllowedValue;
            }
            set
            {
                base.AllowedValue = value;
            }
        }

        public override bool HandlesProperty(string propName)
        {
            return base.HandlesProperty(propName) || this.Properties.ContainsAbsolute(propName);
        }
    }
}

