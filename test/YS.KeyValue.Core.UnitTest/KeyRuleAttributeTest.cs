using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YS.KeyValue.Core.UnitTest
{
    [TestClass]
    public class KeyRuleAttributeTest
    {
        [DataTestMethod]
        [DataRow("abc")]
        [DataRow("abc.bcd")]
        [DataRow("abc.bcd.cde")]
        [DataRow("_.bcd")]
        [DataRow("_")]
        public void ShouldValidateSuccess(string category)
        {
            var attr = new KeyRuleAttribute();
            Assert.IsTrue(attr.IsValid(category));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void ShouldValidateFailure(string category)
        {
            var attr = new KeyRuleAttribute();
            Assert.IsFalse(attr.IsValid(category));
        }
    }
}
