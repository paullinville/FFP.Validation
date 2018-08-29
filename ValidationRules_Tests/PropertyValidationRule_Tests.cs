using FFP.Validations;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ValidationRules_Tests
{
    public class PropertyValidationRule_Tests
    {
        [Fact]
        public void ValidationRule_Ctor()
        {
            ValidationRule<Tested1> tObj = new ValidationRule<Tested1>("TestName", "TestDesc");
            Assert.Equal("TestName", tObj.RuleName);
            Assert.Equal("TestDesc", tObj.Description);
        }

        [Fact]
        public void NotEmptyStringValidation_Broke()
        {
            Tested1 validated = new Tested1();
            IValidationRule tObj = CommonPropRules.NotEmptyStringValidation("StringProp");
            Assert.True(tObj.IsBroken(validated));
        }


        [Fact]
        public void NotEmptyStringValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { StringProp = "Test" };
            IValidationRule tObj = CommonPropRules.NotEmptyStringValidation("StringProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void NotEmptyGuidValidation_Broke()
        {
            Tested1 validated = new Tested1();
            IValidationRule tObj = CommonPropRules.GuidNotEmpty("GuidProp");
            Assert.True(tObj.IsBroken(validated));
        }


        [Fact]
        public void NotEmptyGuidValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.NewGuid() };
            IValidationRule tObj = CommonPropRules.GuidNotEmpty("GuidProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void GuidEmptyValidation_Broke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.NewGuid() };
            IValidationRule tObj = CommonPropRules.GuidEmpty("GuidProp");
            Assert.True(tObj.IsBroken(validated));
        }

        [Fact]
        public void GuidEmptyValidation_NotBroke()
        {
            Tested1 validated = new Tested1() { GuidProp = Guid.Empty };
            IValidationRule tObj = CommonPropRules.GuidEmpty("GuidProp");
            Assert.False(tObj.IsBroken(validated));
        }

        [Fact]
        public void GreaterThan_Broke()
        {
            Tested1 validated = new Tested1() { IntProp = 1 };
            IValidationRule tObj = CommonPropRules.GreaterThanValidation(1, "IntProp");
            Assert.True(tObj.IsBroken(validated));
        }

        [Fact]
        public void GreaterThan_NotBroke()
        {
            Tested1 validated = new Tested1() { IntProp = 2 };
            Tested1 val2 = new Tested1() { IntProp = 0 };
            IValidationRule tObj = CommonPropRules.GreaterThanValidation(1, "IntProp");
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
            Assert.Equal("three", tObj.PropertyValue(new Tested3()));
        }

    }



    public class Tested3
    {
        public string StringProp { get; set; } = "three";
    }


    public class Tested2 : Tested1
    {
        public Tested2()
        {
            StringProp = "Tests2 Value";
        }
    }

    public class Tested1 : IValidatedItem
    {
        public string StringProp { get; set; }
        public Guid GuidProp { get; set; }
        public int IntProp { get; set; }
        public decimal DecimalProp { get; set; }

        public Guid BOID => throw new NotImplementedException();
        private List<IValidationRule> rules = new List<IValidationRule>();
        public void AddRule(IValidationRule Validation)
        {
            rules.Add(Validation);
        }

        public string FriendlyName()
        {
            return "Tested1";
        }

        private BrokenValidationRules broken;
        public IEnumerable<IValidationRule> InvalidRules(bool recheck = true)
        {
            if (broken == null || recheck)
            {
                broken = new BrokenValidationRules();
                foreach (var itm in rules)
                {
                    if (itm.IsBroken(this))
                    {
                        broken.Add(itm);
                    }
                }
            }
            return broken;
        }
    }

}
