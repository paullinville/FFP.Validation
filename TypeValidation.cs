using Observer;
using System;
using System.Collections.Generic;
namespace Validations
{
    public abstract class TypeValidation : IValidation, IPublisher
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
            foreach (IRuleDescription rle in ValidationList)
            {
                if (EventChannels.Publish<ValidateCEvent>(new ValidateCEvent(this, "CheckRule", rle)).CheckRule)
                {
                    if (rle is IRule)
                    {
                        if (((IRule)rle).IsBroken(bo))
                            broke.Add(new BrokenRule(rle, bo));
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

