﻿namespace FFP.Validations
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Validation"></param>
    /// <returns>True if Validation is broken. False if Validation is not broken.</returns>
    /// <remarks></remarks>
    public delegate bool ValidationHandler<t>(IValidationRule Validation, t objectChecked);
    public delegate bool PropValidationHandler<t,u>(IPropertyValidationRule<t, u> Validation, object objectChecked);
}
