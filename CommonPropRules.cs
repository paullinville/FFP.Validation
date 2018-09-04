using FFP.CoreUtilities;
using System;

namespace FFP.Validations
{

    public class CommonPropRules
    {
        public const string EqualToValidationName = "Equal To";

        public static IRule NotNothingValidation(string propName, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<object, object> newValidation = new ClientSideValidationRule<object, object>("Not Nothing",
                                                                                                            "Value must be set.",
                                                                                                            propName,
                                                                                                            CommonPropRuleHandlers.NotNullHandler);
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule DateNotMinValidation(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<DateTime, object> newValidation = new ClientSideValidationRule<DateTime, object>("Date Not Min",
                "Date must be set.",
                propname,
                CommonPropRuleHandlers.DateNotMinHandler);

            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule DateNotMaxValidation(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<DateTime, object> newValidation = new ClientSideValidationRule<DateTime, object>("Date Not Max",
                "Date must be less than " + DateTime.MaxValue.ToString() + ".",
                propname,
                CommonPropRuleHandlers.DateNotMinHandler);
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule MaxStringLengthValidation(string propname, int maxLength, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<string, int> newValidation = new ClientSideValidationRule<string, int>("Max String Length",
                                                                          "Length of string cannot exceed " + maxLength.ToString() + " characters.",
                                                                          propname,
                                                                          CommonPropRuleHandlers.MaxStringLengthHandler);
            newValidation.AllowedValue = maxLength;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule MinStringLengthValidation(string propname, int minLength, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<string, int> newValidation = new ClientSideValidationRule<string, int>("Min String Length",
                                                                            "Length of string must be at least " + minLength.ToString() + " characters.",
                                                                            propname,
                                                                            CommonPropRuleHandlers.MinStringLengthHandler);

            newValidation.AllowedValue = minLength;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule NotEmptyStringValidation(string PropName = null, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<string, string> newValidation = new ClientSideValidationRule<string, string>("Not Empty",
                                                                          "Value cannot be empty.",
                                                                           PropName,
                                                                           CommonPropRuleHandlers.NonBlankStringHandler);

            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule GuidNotEmpty(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<Guid, Guid> newValidation = new ClientSideValidationRule<Guid, Guid>("GUID Not Empty",
                                                                            "Item cannot be empty.",
                                                                            propname,
                                                                            CommonPropRuleHandlers.NonEmptyGuidHandler);
            newValidation.Severity = severity;
            return newValidation;
        }


        public static IRule GuidEmpty(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<Guid, Guid> newValidation = new ClientSideValidationRule<Guid, Guid>("GUID Empty",
                                                                                                  "Item must be empty.",
                                                                                                  propname,
                                                                                                  CommonPropRuleHandlers.EmptyGuidHandler);

            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule GreaterThanValidation(string propname, IComparable allowedValue, ValidationSeverity severity = ValidationSeverity.Critical)
        {

            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>("Greater Than",
                                                                            "Value must be greater than " + allowedValue?.ToString() + ".",
                                                                            propname,
                                                                            CommonPropRuleHandlers.GreaterThanHandler);
            newValidation.AllowedValue = allowedValue;
            newValidation.Severity = severity;
            return newValidation;
        }


        public static IRule LessThanValidation(string propname, IComparable value, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>("Less Than",
                                                                                                    "Value must be less than " + value?.ToString() + ".",
                                                                                                    propname,
                                                                                                    CommonPropRuleHandlers.LessThanHandler);

            newValidation.AllowedValue = value;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule GreaterThanOrEqualToValidation(string propname, IComparable value, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>("Greater Than Or Equal To",
                                                                          "Value must be greater than or equal to " + value?.ToString() + ".",
                                                                          propname,
                                                                          CommonPropRuleHandlers.GreaterThanOrEqualToHandler);

            newValidation.AllowedValue = value;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule LessThanOrEqualToValidation(string propname, IComparable value, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>("Less Than Or Equal To",
                                                                          "Value must be less than or equal to " + value?.ToString() + ".",
                                                                          propname,
                                                                          CommonPropRuleHandlers.LessThanOrEqualToHandler);

            newValidation.AllowedValue = value;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule BetweenValidation(string propname, IComparable fromVal, IComparable toval, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, ValueRangeValidationHelper> newValidation = new ClientSideValidationRule<IComparable, ValueRangeValidationHelper>("Between",
                                                                            "Value must be between " + fromVal?.ToString() + " and " + toval?.ToString() + ".",
                                                                            propname,
                                                                            CommonPropRuleHandlers.BetweenValueHandler);

            newValidation.AllowedValue = new ValueRangeValidationHelper(fromVal, toval);
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule BetweenValidation(string propname, IDateRange range, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<DateTime, IDateRange> newValidation = new ClientSideValidationRule<DateTime, IDateRange>("Date Between",
                                                                                                                      "Date must be between " + range.FirstValidDate.ToString() + " and " + range.LastValidDate.ToString() + ".",
                                                                                                                      propname,
                                                                                                                       CommonPropRuleHandlers.DateRangeHandler);
            newValidation.AllowedValue = range;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule NumberRangeValidationAllowMin(string propname, byte intPartLength, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            decimal number;
            if (intPartLength == 0)
                number = 0.9999999999999999999999999999M;
            else
            {
                string numStr = "";
                for (byte i = 1; i <= intPartLength; i++)
                    numStr += "9";
                number = System.Convert.ToDecimal(numStr);
            }


            ValueRangeValidationHelper range = new ValueRangeValidationHelper((number * -1), number);
            ClientSideValidationRule<IComparable, ValueRangeValidationHelper> newValidation;
            newValidation = new ClientSideValidationRule<IComparable, ValueRangeValidationHelper>("Number Range",
                                                                                                "Number must be between " + (number).ToString() + " and " + (number * -1).ToString() + ".",
                                                                                                propname,
                                                                                                CommonPropRuleHandlers.BetweenValueHandlerAllowMinValue);
            ;
            newValidation.AllowedValue = range;
            newValidation.Severity = severity;
            return newValidation;
        }


        public static IRule NumberRangeValidation(string propname, byte intPartLength, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            decimal number;
            if (intPartLength == 0)
                number = 0.9999999999999999999999999999M;
            else
            {
                string numStr = "";
                for (byte i = 1; i <= intPartLength; i++)
                    numStr += "9";

                numStr = numStr + ".";
                numStr = numStr.PadRight(29, '9');
                number = System.Convert.ToDecimal(numStr);
            }



            ValueRangeValidationHelper range = new ValueRangeValidationHelper((number * -1), number);
            ClientSideValidationRule<IComparable, ValueRangeValidationHelper> newValidation;
            newValidation = new ClientSideValidationRule<IComparable, ValueRangeValidationHelper>("Number Range",
                                                                                              "Number must be between " + (number).ToString() + " and " + (number * -1).ToString() + ".",
                                                                                              propname,
                                                                                              CommonPropRuleHandlers.BetweenValueHandler);
            newValidation.AllowedValue = range;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule EqualToValidation(string propname, IComparable value, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>(EqualToValidationName,
                                                                          "Value must be equal to " + value?.ToString() + ".",
                                                                          propname,
                                                                          CommonPropRuleHandlers.EqualToHandler);

            newValidation.AllowedValue = value;
            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule NotEqualToValidation(string propname, IComparable value, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<IComparable, IComparable> newValidation = new ClientSideValidationRule<IComparable, IComparable>("Not Equal To",
                                                    "Value must not be equal to " + value.ToString() + ".",
                                                    propname,
                                                    CommonPropRuleHandlers.NotEqualToHandler);
            newValidation.AllowedValue = value;
            newValidation.Severity = severity;
            return newValidation;
        }


        public static IRule IsNotNumeric(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<object, object> newValidation;
            newValidation = new ClientSideValidationRule<object, object>("Is Not Numeric",
                                                                        "Value {0} is not a valid number.".FormatStr(propname),
                                                                        propname,
                                                                        CommonPropRuleHandlers.IsNumberBroke);

            newValidation.Severity = severity;
            return newValidation;
        }

        public static IRule MustBeNullOrNumeric(string propname, ValidationSeverity severity = ValidationSeverity.Critical)
        {
            ClientSideValidationRule<object, object> newValidation = new ClientSideValidationRule<object, object>("Must Be Null Or Numeric",
                                                                                                            "Value {0} is not a valid number.".FormatStr(propname),
                                                                                                            propname,
                                                                                                            CommonPropRuleHandlers.IsNumberBroke);
            newValidation.Severity = severity;
            return newValidation;
        }

    }
}
