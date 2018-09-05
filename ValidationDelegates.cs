namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Rule"></param>
    /// <returns>True if Validation is broken. False if Validation is not broken.</returns>
    /// <remarks></remarks>
    public delegate bool ValidationDelegate<t>(IRuleDescription Rule, t objectChecked);
    public delegate bool PropValidationDelegate<t,u>(IPropertyRule<t, u> Validation, object objectChecked);

   
}

