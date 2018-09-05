using System;
using System.Collections.Generic;
using System.Reflection;
using Validations;
using Xunit;

namespace ValidationRules_Tests
{
    public class ValidationRule_Tests
    {
        [Fact]
        public void CtorTest()
        {
            ValidatedItemRule tObj = new ValidatedItemRule("testName", "testDescription");
            
        }
       
        [Fact]
        public void Poco_Rule_Ctor()
        {
           // POCOValidationRule tObj = new POCOValidationRule();
        }

        
    }
}
