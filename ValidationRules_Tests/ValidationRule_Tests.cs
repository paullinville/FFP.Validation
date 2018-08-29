using FFP.Validations;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ValidationRules_Tests
{
    public class ValidationRule_Tests
    {
        [Fact]
        public void CtorTest()
        {
            ValidationRule<IValidatedItem> tObj = new ValidationRule<IValidatedItem>("testName", "testDescription");
            
        }
    }
}
