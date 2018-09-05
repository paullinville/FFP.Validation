using FFP.CoreUtilities;
using System;

namespace Validations
{
    public class ClientSideValidationRule<t, u> : PropertyValidationRule<t, u>, IClientValidation
    {

        public ClientSideValidationRule(string ruleName, string Description, string PropName, PropValidationDelegate<t, u> handler) : base(ruleName, Description, PropName, handler)
        {
            
        }
        
        public string JavaScript()
        {
            System.Text.StringBuilder bldr = new System.Text.StringBuilder();
           
            bldr.Append("function(value) {if (");
            bldr.Append(RuleName.Replace(" ", "_"));
            bldr.Append("(value");

            if (AllowedValue != null)
                bldr.Append(string.Format(", {0}))", AllowedValueString()));
            else
                bldr.Append("))");
            bldr.Append(" return ");
            bldr.Append(Description.Surround("'"));

            bldr.Append("; }");
            return bldr.ToString();
        }


        public string AllowedValueString()
        {
            if (AllowedValue is ValueRangeValidationHelper)
                return ""; //todo fix string.Format("{0}, {1}", TypeToString(((ValueRangeValidationHelper)AllowedValue).FromValue), TypeToString(((ValueRangeValidationHelper)AllowedValue).ToValue));
            else if (AllowedValue is IDateRange)
                return string.Format("{0}, {1}", ((IDateRange)AllowedValue).FirstValidDate.JavaScriptDate(), ((IDateRange)AllowedValue).LastValidDate.JavaScriptDate());
            else
                return TypeToString(AllowedValue);
        }


        public string TypeToString(object val)
        {
            if (typeof(u) == typeof(DateTime))
                return (System.Convert.ToDateTime(AllowedValue)).JavaScriptDate();
            else if (val is bool)
                return AllowedValue.ToString().ToLower();
            else if (val is string)
                return System.Convert.ToString(AllowedValue);
            else if (val is decimal && System.Convert.ToDecimal(val) != 0)
            {
                string tempVal = val.ToString();
                string leftVal = tempVal.BeforeX(".", false);

                if (leftVal.IsNotNullOrBlank())
                {
                    string rightVal = tempVal.AfterX(".", false);

                    int numberDecimals;

                    if (leftVal.StartsWith("-"))
                        numberDecimals = 17 - leftVal.Length;
                    else
                        numberDecimals = 16 - leftVal.Length;

                    if (numberDecimals > 0)
                        return leftVal + "." + rightVal.Left(numberDecimals);
                    else
                        return leftVal + "." + rightVal;
                }
                else
                    return tempVal;
            }
            else
                return val.ToString();
        }
    }
}

