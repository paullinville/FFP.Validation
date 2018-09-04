using FFP.Validations;
using System;
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
            IRule tObj = CommonPropRules.GreaterThanValidation("IntProp", 1);
            Assert.True(tObj.IsBroken(validated));
        }

        [Fact]
        public void GreaterThan_NotBroke()
        {
            Tested1 validated = new Tested1() { IntProp = 2 };
            Tested1 val2 = new Tested1() { IntProp = 0 };
            IRule tObj = CommonPropRules.GreaterThanValidation("IntProp", 1);
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

        [Theory]
        [InlineData("", null, " ", "a")]
        public void POCO_ValidationMultipleTests(string a, string b, string c, string d)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotEmptyStringValidation("StringProp");

            tObj.StringProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = b;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = c;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = d;
            Assert.False(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, "", "   ", "a", "aa", "aaa")]
        public void MinStringLength(string a, string b, string c, string d, string e, string f)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.MinStringLengthValidation("StringProp", 2);

            tObj.StringProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = b;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = c;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = d;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = e;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = f;
            Assert.False(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, "", "   ", "a", "aa", "aaa", " a  ")]
        public void MaxStringLength(string a, string b, string c, string d, string e, string f, string g)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.MaxStringLengthValidation("StringProp", 2);

            tObj.StringProp = a;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = b;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = c;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = d;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = e;
            Assert.False(rule.IsBroken(tObj));

            tObj.StringProp = f;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = g;
            Assert.False(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, "")]
        public void NotNothingRule_String(string a, string b)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotNothingValidation("StringProp");

            tObj.StringProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.StringProp = b;
            Assert.False(rule.IsBroken(tObj));
        }


        [Theory]
        [InlineData(null, 0)]
        public void NotNothingRule_Nullabe(int? a, int? b)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotNothingValidation("IntNullableProp");

            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = b;
            Assert.False(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, -1, 0, 1, 2)]
        public void GreaterThan_Nullabe(int? a, int? b, int? c, int? d, int? e)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.GreaterThanValidation("IntNullableProp", 1);

            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = b;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = c;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = d;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = e;
            Assert.False(rule.IsBroken(tObj));

            rule = CommonPropRules.GreaterThanValidation("IntNullableProp", null);
            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, -1, 0, 1, 2)]
        public void LessThan_Nullabe(int? a, int? b, int? c, int? d, int? e)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.LessThanValidation("IntNullableProp", 1);

            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = b;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = c;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = d;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = e;
            Assert.True(rule.IsBroken(tObj));

            rule = CommonPropRules.LessThanValidation("IntNullableProp", null);
            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, -1, 0, 1, 2)]
        public void GreaterThanOrEqual_Nullabe(int? a, int? b, int? c, int? d, int? e)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.GreaterThanOrEqualToValidation("IntNullableProp", 1);

            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = b;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = c;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = d;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = e;
            Assert.False(rule.IsBroken(tObj));

            rule = CommonPropRules.GreaterThanOrEqualToValidation("IntNullableProp", null);
            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));
        }

        [Theory]
        [InlineData(null, -1, 0, 1, 2)]
        public void LessThanOrEqual_Nullabe(int? a, int? b, int? c, int? d, int? e)
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.LessThanOrEqualToValidation("IntNullableProp", 1);

            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = b;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = c;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = d;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = e;
            Assert.True(rule.IsBroken(tObj));

            rule = CommonPropRules.LessThanValidation("IntNullableProp", null);
            tObj.IntNullableProp = a;
            Assert.True(rule.IsBroken(tObj));
        }

        [Fact]
        public void BetweenValidation_Nullabe()
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.BetweenValidation("IntNullableProp", 1, 3);

            tObj.IntNullableProp = null;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = 0;
            Assert.True(rule.IsBroken(tObj));

            tObj.IntNullableProp = 1;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = 2;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = 3;
            Assert.False(rule.IsBroken(tObj));

            tObj.IntNullableProp = 4;
            Assert.True(rule.IsBroken(tObj));

            rule = CommonPropRules.BetweenValidation("IntNullableProp", null, null);
            tObj.IntNullableProp = 1;
            Assert.True(rule.IsBroken(tObj));
        }

        [Fact]
        public void DateNotMinValidation()
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.DateNotMinValidation("DateProp");

            Assert.True(rule.IsBroken(tObj));

            tObj.DateProp = null;
            Assert.True(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MinValue;
            Assert.True(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MinValue.AddMilliseconds(1);
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.Now;
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MaxValue;
            Assert.False(rule.IsBroken(tObj));
        }

        public void DateNotMaxValidation()
        {
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.DateNotMaxValidation("DateProp");

            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = null;
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MinValue;
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MaxValue.AddMilliseconds(-1);
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.Now;
            Assert.False(rule.IsBroken(tObj));

            tObj.DateProp = DateTime.MaxValue;
            Assert.True(rule.IsBroken(tObj));

        }

        [Fact]
        public void NotNothing_Ref()
        {
            Tested2 value = null;
            Tested3 tObj = new Tested3();
            IRule rule = CommonPropRules.NotNothingValidation("RefProp");

            Assert.True(rule.IsBroken(tObj));

            value = new Tested2();
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

}
