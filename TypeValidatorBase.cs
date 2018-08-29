using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFP.Validations
{
    public abstract class TypeValidatorBase<T> : ITypeValidation
    {
        public TypeValidatorBase()
        {
            Validation = AddTypeValidation();
        }

        public TypeValidatorBase(string name, string description) : this()
        {
            Validation.RuleName = name;
            Validation.Description = description;
        }

        protected IValidationRule Validation { get; set; }


        public virtual void CheckRules(IValidatedItem bo)
        {
            if (Validation != null)
                bo.AddRule(Validation);
        }

        public abstract bool ValidationBrokeHandler<u>(IValidationRule target, u objectChecked) where u: IValidatedItem;

        public virtual IEnumerable<Type> TypeFor => new[] { typeof(T) };

        protected virtual IValidationRule AddTypeValidation()
        {
            return new ValidationRule<IValidatedItem>(ValidationName(), ValidationDescription, ValidationBrokeHandler<IValidatedItem>);
        }

        protected virtual string ValidationName()

        {
            return this.GetType().Name.BreakByCapitols();
        }

        protected virtual string ValidationDescription { get; set; }


        public virtual bool IsFor(object objFor)
        {
            return TypeFor.Contains(objFor.GetType());
        }

        public abstract IEnumerable<IValidationRule> ValidationRules();
    }
}
