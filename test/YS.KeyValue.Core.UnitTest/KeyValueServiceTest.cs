using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.KeyValue.Core.UnitTest.Entity;
using YS.Knife.Hosting;
using Moq;
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
        [TestCategory("Generic")]
        public void ShouldThrowExceptionWhenKeyValueEntityIsGenericType()
        {
            var exception = Assert.ThrowsException<NotSupportedException>(() =>
            {
                this.GetRequiredService<IKeyValueService<GenericType<string>>>();
            });
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        [TestCategory("Generic")]
        public void ShouldThrowExceptionWhenKeyValueEntityIsNestedType()
        {
            var exception = Assert.ThrowsException<NotSupportedException>(() =>
            {
                this.GetRequiredService<IKeyValueService<NestedType>>();
            });
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        [TestCategory("Generic")]
        public async Task ShouldInvokeProviderGetByKey()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
            await keyValue.GetByKey("input_key");
            Mock.Get(keyValueProvider).Verify(p => p.GetByKey<Person>(typeof(Person).FullName.ToLower(), "input_key"), Times.Once);
        }

        [TestMethod]
        [TestCategory("Generic")]
        public async Task ShouldInvokeProviderListAll()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
            await keyValue.ListAll();
            Mock.Get(keyValueProvider).Verify(p => p.ListAll<Person>(typeof(Person).FullName.ToLower()), Times.Once);
        }
        [TestMethod]
        [TestCategory("Generic")]
        public async Task ShouldInvokeProviderDeleteKey()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
            await keyValue.DeleteByKey("delete_key");
            Mock.Get(keyValueProvider).Verify(p => p.DeleteByKey<Person>(typeof(Person).FullName.ToLower(), "delete_key"), Times.Once);
        }
        [TestMethod]
        [TestCategory("Generic")]
        public async Task ShouldInvokeProviderAddOrUpdate()
        {
            var keyValue = this.GetRequiredService<IKeyValueService<Person>>();
            var person = new Person();
            await keyValue.AddOrUpdate("key", person);
            Mock.Get(keyValueProvider).Verify(p => p.AddOrUpdate<Person>(typeof(Person).FullName.ToLower(), "key", person), Times.Once);
        }

        public class NestedType
        {
            public int Value { get; set; }
        }
    }
}
