using FFP.CoreUtilities;
using System.Collections.Generic;
using System.Linq;

namespace FFP.Validations
{
    public class ValidatedBase : List<IRule>, IValidatedItem
    {
        
        private List<IRule> InternalRules { get; set; } = new List<IRule>();

        public void AddRule(IRule Validation)
        {
            Add(Validation);
        }

        private BrokenValidationRules broke { get; set; }

        private BrokenValidationRules brokenRules(bool rechecking)
        {
            if (broke == null)
            {
                broke = new BrokenValidationRules();
            }
            else if (rechecking)
            {
                broke.Clear();
            }
            return broke;
        }

        public IEnumerable<IBrokenRule> InvalidRules(bool recheck = true)
        {
            brokenRules(recheck).AddRange(ValidationPackages.Validate(this));
            InternalRules.Where((x) => x.IsBroken(this)).ForEach((x) => broke.Add(new BrokenRule(x, this)));
            return broke;
        }

        public bool IsValid()
        {
            return InvalidRules(true).IsEmpty();
        }
    }

}

