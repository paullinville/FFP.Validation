using System;

namespace Validations
{
    public class ValueRangeValidationHelper
    {
        public IComparable FromValue;
        public IComparable ToValue;
        public ValueRangeValidationHelper(IComparable fromval, IComparable toval)
        {
            FromValue = fromval;
            ToValue = toval;
        }
    }
}

