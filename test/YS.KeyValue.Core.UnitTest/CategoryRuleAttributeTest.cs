using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YS.KeyValue.Core.UnitTest
{
    [TestClass]
    public class CategoryRuleAttributeTest
    {
        [DataTestMethod]
        [DataRow("abc")]
        [DataRow("abc.bcd")]
        [DataRow("abc.bcd.cde")]
        [DataRow("_.bcd")]
        [DataRow("_")]
        public void ShouldValidateSuccess(string category)
        {
            var attr = new CategoryRuleAttribute();
            Assert.IsTrue(attr.IsValid(category));
        }

        [DataTestMethod]
        [DataRow("a b")]
        [DataRow("a..b")]
        [DataRow("sdf&")]
        [DataRow("")]
        public void ShouldValidateFailure(string category)
        {
            var attr = new CategoryRuleAttribute();
            Assert.IsFalse(attr.IsValid(category));
        }
    }
}
