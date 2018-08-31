using Observer;
using System;
using System.Collections.Generic;
namespace FFP.Validations
{
    public abstract class TypeValidation : ITypeValidation, IPublisher
    {
        public TypeValidation()
        {
            ValidationList = new SimpleRuleList();
        }

        protected SimpleRuleList ValidationList { get; }

        public abstract IEnumerable<Type> TypeFor { get; }

        public IEnumerable<IRule> ValidationRules()
        {
            return ValidationList;
        }

        public IEnumerable<IBrokenRule> Validate(object bo)
        {
            List<IBrokenRule> broke = new List<IBrokenRule>();
            foreach (IRule rle in ValidationList)
            {
                if (EventChannels.Publish<ValidCE>(new ValidCE(this, "CheckRule", rle)).CheckRule)
                {
                    if (rle is IValidationRule)
                    {
                        if (((IValidationRule)rle).IsBroken(bo))
                            broke.Add(new BrokenValidationRule(rle, bo));
                    }

                }

            }
            return broke;
        }

        public void AddRule(IRule rule)
        {
            ValidationList.Add(rule);
        }
    }
}

