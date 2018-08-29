using FFP.BO.Interfaces;
using System.Collections.Generic;

namespace FFP.Validations
{
    public abstract class TypeValidator<T> : TypeValidatorBase<T>
    {
        public TypeValidator() : base()
        {
        }

        public TypeValidator(string name, string description) : base(name, description)
        {
        }

        public override IEnumerable<IValidationRule> ValidationRules()
        {
                return new[] { Validation };
        }
    }
}
