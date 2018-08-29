using System;

namespace FFP.Validations
{
    [Serializable()]
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

