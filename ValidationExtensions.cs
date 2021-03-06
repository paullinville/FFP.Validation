﻿using FFP.CoreUtilities;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Validations
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this BrokenValidationRules val)
        {
            return val == null || !val.HasCritical();
        }

        public static string BrokenValidations(this IEnumerable<IRule> val)
        {
            if (val.IsEmpty())
                return "";
            else
            {
                System.Text.StringBuilder obj = new System.Text.StringBuilder();

                bool first = true;

                foreach (IRule item in val)
                {
                    if (first)
                        first = false;
                    else
                    {
                        obj.Append(Constants.vbCrLf);
                        obj.Append(Constants.vbCrLf);
                    }

                    obj.Append(item.Description);
                }
                return obj.ToString().Trim();
            }
        }

        public static string BrokenValidationsString(this IEnumerable<IRule> val, IValidatedItem vitm)
        {
            return BrokenValidations(from itm in val.Values()
                                     where itm.IsBroken(vitm)
                                     select itm);
        }

        public static string BrokenValidationsString(this IEnumerable<IRule> val, IValidatedItem vitm, ValidationSeverity severity)
        {
            return BrokenValidations(from itm in val.Values()
                                     where itm.IsBroken(vitm) && itm.Severity == severity
                                     select itm);
        }

        public static BrokenRule CheckValidation(this IRule val, IValidatedItem bo)
        {
            BrokenRule Validation = null;

            if (val.IsBroken(bo))
                Validation = new BrokenRule(val, bo);
            return Validation;

        }

        public static bool HasCritical(this BrokenValidationRules val)
        {
            if (val == null)
                return false;
            else
            {
                foreach (IRule rle in val)
                {
                    if (rle.Severity == ValidationSeverity.Critical)
                        return true;
                }
                return false;
            }
        }
    }
}

