using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife.Hosting;
using YS.KeyValue.Core.UnitTest.Entity;

namespace YS.KeyValue.Core.UnitTest
{
    [TestClass]
    public class KeyValueFactoryTest : KnifeHost
    {
        public KeyValueFactoryTest() :
            base(new string[0], (hbc, sc) =>
            {
                sc.AddSingleton(Moq.Mock.Of<IKeyValueProvider>());
            })
        {
            this.keyValueFactory = this.GetRequiredService<IKeyValueFactory>();
            this.keyValueProvider = this.GetRequiredService<IKeyValueProvider>();
        }
        private readonly IKeyValueFactory keyValueFactory;
        private readonly IKeyValueProvider keyValueProvider;
        [TestMethod]
        public void ShouldCreateKeyValueInstance()
        {
            var keyValue = keyValueFactory.CreateKeyValue<Person>("key");
            Assert.IsNotNull(keyValue);
        }

        //[TestMethod]
        //public void ShouldThrowExceptionWhenKeyValueEntityIsGenericType()
        //{
        //    var exception = Assert.ThrowsException<NotSupportedException>(() =>
        //    {
        //        keyValueFactory.CreateKeyValue<GenericType<string>>("key");
        //    });
        //    Assert.IsNotNull(exception);
        //}
        //[TestMethod]
        //public void ShouldThrowExceptionWhenKeyValueEntityIsNestedType()
        //{
        //    var exception = Assert.ThrowsException<NotSupportedException>(() =>
        //    {
        //        keyValueFactory.CreateKeyValue<NestedType>("key");
        //    });
        //    Assert.IsNotNull(exception);
        //}


        public class NestedType
        {
            public int Value { get; set; }
        }

    }
}
