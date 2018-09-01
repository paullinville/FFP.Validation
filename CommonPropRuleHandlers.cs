using FFP.BO.Interfaces;
using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{
    public class CommonPropRuleHandlers
    {

        public static bool NonEmptyGuidHandler(IPropertyRule<Guid, Guid> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).IsEmpty())
                return true;
            else
                return false;
        }

        public static bool EmptyGuidHandler(IPropertyRule<Guid, Guid> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).IsEmpty())
                return false;
            else
                return true;
        }

        public static bool BetweenValueHandler(IPropertyRule<IComparable, ValueRangeValidationHelper> target, object objectChecked)
        {
            if (target.AllowedValue == null)
                return false;

            ValueRangeValidationHelper valRange = (ValueRangeValidationHelper)target.AllowedValue;
            IComparable val = target.PropertyValue(objectChecked);
            return !val.Between(valRange.FromValue, valRange.ToValue);
        }

        public static bool BetweenValueHandlerAllowMinValue(IPropertyRule<IComparable, ValueRangeValidationHelper> target, object objectChecked)
        {
            if (target.AllowedValue == null)
                return false;

            ValueRangeValidationHelper valRange = target.AllowedValue;
            IComparable val = target.PropertyValue(objectChecked);

            return !val.Between(valRange.FromValue, valRange.ToValue);
        }

        public static bool DateRangeHandler(IPropertyRule<DateTime, IDateRange> target, object objectChecked)
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

        public static bool LessThanHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if ((target.PropertyValue(objectChecked)).CompareTo(target.AllowedValue) >= 0)
                return true;
            else
                return false;
        }

        public static bool DateNotMinHandler(IPropertyRule<DateTime, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == DateTime.MinValue)
                return true;
            else
                return false;
        }

        public static bool DateNotMaxHandler(IPropertyRule<DateTime, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == DateTime.MaxValue)
                return true;
            else
                return false;
        }

        public static bool NotNullHandler(IPropertyRule<object, object> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return true;
            else
                return false;
        }

        public static bool GreaterThanHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) <= 0)
                return true;
            else
                return false;
        }

        public static bool LessThanOrEqualToHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) <= 0)
                return false;
            else
                return true;
        }

        public static bool EqualToHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) == 0)
                return false;
            else
                return true;
        }

        public static bool NotEqualToHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo((IComparable)target.AllowedValue) != 0)
                return false;
            else
                return true;
        }

        public static bool GreaterThanOrEqualToHandler(IPropertyRule<IComparable, IComparable> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked).CompareTo(target.AllowedValue) >= 0)
                return false;
            else
                return true;
        }

        public static bool NonBlankStringHandler(IPropertyRule<String, string> target, object objectChecked)
        {
            String propVal = target.PropertyValue(objectChecked);
            if (propVal != null)
                return propVal.ToString().IsEmpty();
            else
                return true;
        }


        public static bool MaxStringLengthHandler(IPropertyRule<String, int> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return false;

            if (target.PropertyValue(objectChecked).Length > target.AllowedValue)
                return true;
            else
                return false;
        }

        public static bool MinStringLengthHandler(IPropertyRule<String, int> target, object objectChecked)
        {
            if (target.PropertyValue(objectChecked) == null)
                return true;

            if (target.PropertyValue(objectChecked).Length < target.AllowedValue)
                return true;
            else
                return false;
        }

        public static bool IsNumberBroke(IPropertyRule<Object, object> target, object objectChecked)
        {

            return CoreUtilities.Extensions.IsNumeric(target.PropertyValue(objectChecked).ToString()).IsFalse();
        }


    }
}

