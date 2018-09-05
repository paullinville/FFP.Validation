using Validations;
using System;

namespace ValidationRules_Tests
{
    public class Tested1 : ValidatedBase
    {
        public string StringProp { get; set; }
        public Guid GuidProp { get; set; }
        public int IntProp { get; set; }
        public decimal DecimalProp { get; set; }
    }

}
