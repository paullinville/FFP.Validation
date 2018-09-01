using FFP.CoreUtilities;
using System;
using System.Collections.Generic;

namespace FFP.Validations
{
    public class ValidateItemWrapper : List<IRule>, IValidatedItem
    {
        public ValidateItemWrapper(object ruledItem)
        {
            RuledItem = ruledItem;
        }

        public Guid BOID { get; }

        private object RuledItem { get; set; }

        public void AddRule(IRule Validation)
        {
            Add(Validation);
        }

        public string FriendlyName()
        {
            throw new NotImplementedException();
        }

        private BrokenValidationRules brokenRules { get; set; }


        public IEnumerable<IBrokenRule> InvalidRules(bool recheck = true)
        {
            if (recheck || brokenRules == null)
            {
                brokenRules = new BrokenValidationRules();
                List<IRule> lst = new List<IRule>();
                foreach (IRule rle in this)
                {
                    if (rle.IsBroken(RuledItem))
                    {
                        brokenRules.Add(new BrokenRule(rle, this));
                    }
                }
            }
            return brokenRules;
        }

        public bool IsValid()
        {
            return InvalidRules(true).IsNotEmpty();;
        }
    }


}

