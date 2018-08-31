using FFP.CoreUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFP.Validations
{

    public class ValidationList
    {

        private readonly IValidatedItem mTarget;

        public ValidationList(IValidatedItem target)
        {
            lvBrokenValidations = new BrokenValidationRules();
            mTarget = target;
        }

        private ValidationList()
        {
        }

        public bool ForTarget(IValidatedItem bo)
        {
            if (bo == null || mTarget == null)
                return false;
            else
                return bo.Equals(mTarget);
        }


        public int Count()
        {
            if (lvValidationsList == null)
                return 0;
            else
                return lvValidationsList.Count;
        }

        [NonSerialized()]
        private List<IValidationRule> lvValidationsList;
        public List<IValidationRule> ValidationsList()
        {
            if (lvValidationsList == null)
                lvValidationsList = new List<IValidationRule>();
            return lvValidationsList;
        }

        public void AddValidation(IValidationRule Validation)
        {
            if (Validation == null)
                return;
            else
            {
                ValidationsList().Add(Validation);
            }
        }


        internal void Clear()
        {
            this.ValidationsList().Clear();
            lvBrokenValidations.Clear();
        }


        public void CheckValidationsByProperty(string propName)
        {
            foreach (IValidationRule Validation in ValidationsList())
            {
                //todo fix
                //if (Validation.HandlesProperty(propName))
                //{
                //    if (Validation.IsBroken)
                //        BrokenValidations.Add(Validation);
                //    else
                //        BrokenValidations.Remove(Validation);
                //}
            }
        }

        public void CheckValidations()
        {
            BrokenValidations.Clear();
            foreach (IValidationRule Validation in ValidationsList())
            {
                if (Validation.IsBroken(mTarget))
                    BrokenValidations.Add(new BrokenValidationRule(Validation, null));
            }
        }

        private BrokenValidationRules lvBrokenValidations;
        public BrokenValidationRules BrokenValidations
        {
            get
            {
                return lvBrokenValidations;
            }
        }

        public bool IsValid
        {
            get
            {
                CheckValidations();
                foreach (IValidationRule item in BrokenValidations)
                {
                    if (item.Severity <= ValidationSeverity.Critical)
                        return false;
                }
                return true;
            }
        }

        public bool IsBroken(string ValidationName)
        {
            CheckValidations();
            return BrokenValidations.Contains(ValidationName);
        }

        public BrokenValidationRules GetBrokenValidations()
        {
            CheckValidations();
            return BrokenValidations;
        }

        private string BrokenValidationsString()
        {
            CheckValidations();
            return (from item in BrokenValidations
                    select item.Description).ListToString(",");
        }

        public IEnumerable<IBrokenRule> BrokenValidationsForSeverity(ValidationSeverity severity)
        {
            CheckValidations();
            return from itm in BrokenValidations
                   where itm.Severity == severity
                   select itm;
        }

        public override string ToString()
        {
            return BrokenValidationsString();
        }
    }
}
