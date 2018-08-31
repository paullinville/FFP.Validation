using FFP.BO.Interfaces;
using System;
using System.Collections.Generic;

namespace FFP.Validations
{
    [Serializable()]
    public class BrokenValidationRules : System.Collections.ObjectModel.Collection<IBrokenRule>
    {
        internal new void Add(IBrokenRule Validation)
        {
            if (IgnoreValidation(Validation.RuleName))
                return;
            else if (!Contains(Validation))
                base.Add(Validation);
        }

        private bool IgnoreValidation(string ValidationName)
        {
            //todo fix
            return false;
        }

        public bool Contains(string ValidationName)
        {
            foreach (IValidationRule itm in this)
            {
                if (itm.RuleName == ValidationName)
                    return true;
            }
            return false;
        }

        public void AddValidations(IValidatedItem itm)
        {
            if (itm.IsValid())
                return;

            foreach (IBrokenRule Validation in itm.InvalidRules())
                this.Add(Validation);
        }

        public void AddValidations(IEnumerable<IValidatedItem> itms)
        {
            foreach (IValidatedItem itm in itms)
                AddValidations(itm);
        }

        public void AddRange(IEnumerable<IBrokenRule> brokenValidations)
        {
            foreach (IBrokenRule itm in brokenValidations)
                 Add(itm);
        }
    }
}

