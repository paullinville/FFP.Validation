using FFP.CoreUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFP.Validations
{
    public abstract class BasicValidationPackage : IValidationPackage
    {
        public BasicValidationPackage()
        {
        }

        public Dictionary<Type, List<ITypeValidation>> ValidationRuleTypeDic { get; set; }
        public List<string> GoodGroups { get; set; } = new List<string>();
        public List<string> BadGroups { get; set; } = new List<string>();

        public void Setup()
        {
            AddValidationClasses();
        }

        private IEnumerable<Type> lvTypes;
        public IEnumerable<Type> Types
        {
            get
            {
                if (lvTypes == null)
                    lvTypes = MakeTypes();
                return lvTypes;
            }
            set
            {
                lvTypes = value;
            }
        }

        protected abstract IEnumerable<Type> MakeTypes();


        protected virtual void AddValidationClasses()
        {
            if (ValidationRuleTypeDic == null)
            {
                ValidationRuleTypeDic = new Dictionary<Type, List<ITypeValidation>>();
                foreach (System.Type tp in Types)
                {
                    if (tp.GetInterfaces().Contains(typeof(ITypeValidation)) && tp.IsAbstract.IsFalse() && IsProperGroup(tp))
                    {
                        ITypeValidation classValidations = (ITypeValidation)System.Activator.CreateInstance(tp);
                        foreach (Type clsTp in classValidations.TypeFor)
                        {
                            if (ValidationRuleTypeDic.ContainsKey(clsTp).IsFalse())
                                ValidationRuleTypeDic.Add(clsTp, new List<ITypeValidation>());
                            ValidationRuleTypeDic[clsTp].Add(classValidations);
                        }
                    }
                }
            }
        }

        protected virtual bool IsProperGroup(Type theType)
        {
            if (HasGroups())
            {
                if (IsGoodGroup(theType))
                    return true;

                if (IsBadGroup(theType))
                    return false;
            }
            else
                return true;
            return false;
        }

        protected bool HasGroups()
        {
            if (BadGroups.IsEmpty() && GoodGroups.IsEmpty())
                return false;
            else
                return true;
        }

        protected bool IsBadGroup(Type theType)
        {
            if (BadGroups.IsEmpty())
                return false;
            else if (Attribute.IsDefined(theType, typeof(ValidationGroupAttribute)))
            {
                ValidationGroupAttribute ValidationGroup = (ValidationGroupAttribute)Attribute.GetCustomAttribute(theType, typeof(ValidationGroupAttribute));
                if (ValidationGroup.Groups.Contains(from itm in BadGroups
                                                    select itm.Trim().ToUpper()))
                    return true;
            }
            return false;
        }

        protected bool IsGoodGroup(Type theType)
        {
            if (GoodGroups.IsEmpty())
                return false;
            else if (Attribute.IsDefined(theType, typeof(ValidationGroupAttribute)))
            {
                ValidationGroupAttribute ValidationGroup = (ValidationGroupAttribute)Attribute.GetCustomAttribute(theType, typeof(ValidationGroupAttribute));
                if (ValidationGroup.Groups.Contains(from itm in GoodGroups
                                                    select itm.Trim().ToUpper()))
                    return true;
            }
            return false;
        }

        public abstract string PackageName { get; }

        public override string ToString()
        {
            return PackageName;
        }

        public override int GetHashCode()
        {
            return this.PackageName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IValidationPackage)
            {
                if (((IValidationPackage)obj).PackageName == this.PackageName)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public IEnumerable<IRule> ValidationRules(object obj)
        {
            return TypeValidations(obj.GetType());
        }

        public IEnumerable<IRule> TypeValidations(Type objType)
        {
            List<IRule> lst = new List<IRule>();
            if (ValidationRuleTypeDic.ContainsKey(objType))
            {
                foreach (ITypeValidation itm in ValidationRuleTypeDic[objType])
                    lst.AddRange(itm.ValidationRules());
                return lst;
            }
            return lst;
        }

        public IEnumerable<IBrokenRule> CheckValidationRules(object obj)
        {
            if (ValidationRuleTypeDic.ContainsKey(obj.GetType()))
            {
                BrokenValidationRules broke = new BrokenValidationRules();
                foreach (ITypeValidation clasValidations in ValidationRuleTypeDic[obj.GetType()])
                    broke.AddRange(clasValidations.Validate(obj));
                return broke;
            }
            return null;
        }
    }
}

