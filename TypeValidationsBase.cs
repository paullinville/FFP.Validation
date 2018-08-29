using FFP.CoreUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFP.Validations
{
    public abstract class TypeValidationsBase : ITypeValidation
    {
        public TypeValidationsBase()
        {
            lvValidationList = new SimpleValidationList();
            AddClassValidations();
        }

        private SimpleValidationList lvValidationList;
        public SimpleValidationList ValidationList
        {
            get
            {
                return lvValidationList;
            }
        }

        public void AddValidation(IValidationRule Validation)
        {
            lvValidationList.AddValidation(Validation);
        }

        public virtual void RemoveValidation(string Validationname)
        {
            ValidationList.Remove((from itm in ValidationList
                                   where itm.RuleName == Validationname
                                   select itm).FirstOrDefault());
        }

        public virtual void RemoveValidation(string Validationname, string propertyName)
        {
            ValidationList.Remove((from itm in ValidationList
                                   where itm.RuleName == Validationname
                                   select itm).FirstOrDefault());
        }

        public virtual void RemoveValidation(IValidationRule Validation)
        {
            ValidationList.Remove((from itm in ValidationList
                                   where itm.RuleName.CompareAbsolute(Validation.RuleName)
                                   select itm).FirstOrDefault());
        }

        public virtual void CheckRules(IValidatedItem bo)
        {
            foreach (IValidationRule rle in ValidationList)
            {
                if (rle.IsBroken(bo))
                    bo.AddRule(rle);
            }
        }

        public abstract IEnumerable<Type> TypeFor { get; }

        protected abstract void AddClassValidations();

        public virtual bool IsFor(object objFor)
        {
            return TypeFor.Equals(objFor.GetType());
        }

        public IEnumerable<IValidationRule> ValidationRules()
        {
            List<IValidationRule> lst = new List<IValidationRule>();
            lst.AddRange(ValidationList.Cast<IValidationRule>());
            return lst;
        }
    }
}

