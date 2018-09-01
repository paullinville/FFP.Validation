using FFP.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace ValidationRules_Tests
{
    public class PropertyValidationRule_Tests
    {
        [Fact]
        public void ValidationRule_Ctor()
        {
            ValidatedItemRule tObj = new ValidatedItemRule("TestName", "TestDesc");
            Assert.Equal("TestName", tObj.RuleName);
            Assert.Equal("TestDesc", tObj.Description);
        }

        [Fact]
        public void NotEmptyStringValidation_Broke()
        {
            Tested1 validated = new Tested1();
            IRule tObj = CommonPropRules.NotEmptyStringValidation("StringProp");
            Assert.True(tObj.IsBroken(validated));
        }


        [Fact]
        public void NotEmptyStringValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { StringProp = "Test" };
            IRule tObj = CommonPropRules.NotEmptyStringValidation("StringProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void NotEmptyGuidValidation_Broke()
        {
            Tested1 validated = new Tested1();
            IRule tObj = CommonPropRules.GuidNotEmpty("GuidProp");
            Assert.True(tObj.IsBroken(validated));
        }


        [Fact]
        public void NotEmptyGuidValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.NewGuid() };
            IRule tObj = CommonPropRules.GuidNotEmpty("GuidProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void GuidEmptyValidation_Broke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.NewGuid() };
            IRule tObj = CommonPropRules.GuidEmpty("GuidProp");
            Assert.True(tObj.IsBroken(validated));
        }

        [Fact]
        public void GuidEmptyValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.Empty };
            IRule tObj = CommonPropRules.GuidEmpty("GuidProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void GreaterThan_Broke()
        {
            Tested1 validated = new Tested1() { IntProp = 1 };
            IRule tObj = CommonPropRules.GreaterThanValidation(1, "IntProp");
            Assert.True(tObj.IsBroken(validated));
        }

        [Fact]
        public void GreaterThan_NotBroke()
        {
            Tested1 validated = new Tested1() { IntProp = 2 };
            Tested1 val2 = new Tested1() { IntProp = 0 };
            IRule tObj = CommonPropRules.GreaterThanValidation(1, "IntProp");
            Assert.False(tObj.IsBroken(validated));
            Assert.True(tObj.IsBroken(val2));
        }

        [Fact]
        public void PropertyValue_Tests()
        {
            Tested1 tobj1 = new Tested1() { StringProp = "Test Value" };

            PropertyValidationRule<string, string> tObj = new PropertyValidationRule<string, string>("TestRuleName", "TestDescription", "StringProp");

            Assert.Equal("Test Value", tObj.PropertyValue(tobj1));

            Assert.Equal("Tests2 Value", tObj.PropertyValue(new Tested2()));
            Assert.Null(tObj.PropertyValue(new Tested3()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void POCO_Validation(string val)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotEmptyStringValidation("StringProp");

            tObj.StringProp = val;
            Assert.True(rule.IsBroken(tObj));
        }

        [Fact]
        public void POCO_Validation_Valid()
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotEmptyStringValidation("StringProp");
            tObj.StringProp = "Test";
            Assert.False(rule.IsBroken(tObj));
        }

    }



    public class Tested3
    {
        public string StringProp { get; set; }
    }


    public class Tested2 : Tested1
    {
        public Tested2()
        {
            StringProp = "Tests2 Value";
        }
    }

    public class Tested1 : ValidatedBase
    {
        public string StringProp { get; set; }
        public Guid GuidProp { get; set; }
        public int IntProp { get; set; }
        public decimal DecimalProp { get; set; }
    }

}
