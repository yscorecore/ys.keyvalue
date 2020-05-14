using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.KeyValue.Core.UnitTest.Entity;
using YS.Knife.Hosting;

namespace YS.KeyValue.Core.UnitTest
{
    [TestClass]
    public class KeyValueServiceTest : KnifeHost
    {
        public KeyValueServiceTest() :
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
        [TestCategory("Factory")]
        public void ShouldCreateKeyValueInstanceUseKeyValueFactory()
        {
            var keyValue = keyValueFactory.CreateKeyValue<Person>("key");
            Assert.IsNotNull(keyValue);
        }

        [TestMethod]
        [TestCategory("Generic")]
        public void ShouldCreateGenericKeyValueInstanceFromContainer()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
            Assert.IsNotNull(keyValue);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenKeyValueEntityIsGenericType()
        {
            var exception = Assert.ThrowsException<NotSupportedException>(() =>
            {
                this.GetRequiredService<GenericType<string>>();
            });
            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void ShouldThrowExceptionWhenKeyValueEntityIsNestedType()
        {
            var exception = Assert.ThrowsException<NotSupportedException>(() =>
            {
                this.GetRequiredService<NestedType>();
            });
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        [TestCategory("Generic")]
        public void ShouldInvokeProviderGetByKey()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
        }

        public class NestedType
        {
            public int Value { get; set; }
        }
    }
}
