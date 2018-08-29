using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{
    public class CommonPropRuleHandlers
    {

        public static bool NonEmptyGuidHandler(IPropertyValidationRule<Guid, Guid> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).IsEmpty())
                return true;
            else
                return false;
        }

        public static bool EmptyGuidHandler(IPropertyValidationRule<Guid, Guid> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).IsEmpty())
                return false;
            else
                return true;
        }

        public static bool BetweenValueHandler(IPropertyValidationRule<IComparable, ValueRangeValidationHelper> target, object objectChecked)
        {
            if (target.AllowedValue == null)
                return false;

            ValueRangeValidationHelper valRange = (ValueRangeValidationHelper)target.AllowedValue;
            IComparable val = target.PropertyValue(objectChecked);
            return !val.Between(valRange.FromValue, valRange.ToValue);
        }

        public static bool BetweenValueHandlerAllowMinValue(IPropertyValidationRule<IComparable, ValueRangeValidationHelper> target, object objectChecked)
        {
            if (target.AllowedValue == null)
                return false;

            ValueRangeValidationHelper valRange = target.AllowedValue;
            IComparable val = target.PropertyValue(objectChecked);

            return !val.Between(valRange.FromValue, valRange.ToValue);
        }

        public static bool DateRangeHandler(IPropertyValidationRule<DateTime, IDateRange> target, object objectChecked)
        {
            if (target.AllowedValue != null)
            {
                if (!target.AllowedValue.InRange(target.PropertyValue(objectChecked)))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool LessThanHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if ((target.PropertyValue(objectChecked)).CompareTo(target.AllowedValue) >= 0)
                return true;
            else
                return false;
        }

        public static bool DateNotMinHandler(IPropertyValidationRule<DateTime, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == DateTime.MinValue)
                return true;
            else
                return false;
        }

        public static bool DateNotMaxHandler(IPropertyValidationRule<DateTime, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == DateTime.MaxValue)
                return true;
            else
                return false;
        }

        public static bool NotNullHandler(IPropertyValidationRule<object, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return true;
            else
                return false;
        }

        public static bool GreaterThanHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) <= 0)
                return true;
            else
                return false;
        }

        public static bool LessThanOrEqualToHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) <= 0)
                return false;
            else
                return true;
        }

        public static bool EqualToHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) == 0)
                return false;
            else
                return true;
        }

        public static bool NotEqualToHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo((IComparable)target.AllowedValue) != 0)
                return false;
            else
                return true;
        }

        public static bool GreaterThanOrEqualToHandler(IPropertyValidationRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) >= 0)
                return false;
            else
                return true;
        }

        public static bool NonBlankStringHandler(IPropertyValidationRule<String, string> target, object objectChecked)
        {
            String propVal = target.PropertyValue(objectChecked);
            if (propVal != null)
                return propVal.ToString().IsEmpty();
            else
                return true;
        }


        public static bool MaxStringLengthHandler(IPropertyValidationRule<String, int> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return false;

            if (target.PropertyValue(objectChecked).Length > target.AllowedValue)
                return true;
            else
                return false;
        }

        public static bool MinStringLengthHandler(IPropertyValidationRule<String, int> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return true;

            if (target.PropertyValue(objectChecked).Length < target.AllowedValue)
                return true;
            else
                return false;
        }

        public static bool IsNumberBroke(IPropertyValidationRule<Object, object> target, object objectChecked)
        {

            return CoreUtilities.Extensions.IsNumeric(target.PropertyValue(objectChecked).ToString()).IsFalse();
        }


    }
}

