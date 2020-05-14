using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Hosting;

namespace YS.KeyValue.Impl.Dynamo.UnitTest
{
    [TestClass]
    public class DynamoKeyValueProviderTest : KnifeHost
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public async Task ShouldSuccessWhenAddAValideEntity()
        {
            var provider = this.GetService<IKeyValueProvider>();
            await provider.AddOrUpdate("abcd", "u0001", new Person() { Name = "zhangsan", Age = 12 });
            var person1 = await provider.GetByKey<Person>("abcd", "u0001");
            Assert.IsNotNull(person1);
            Assert.AreEqual("zhangsan", person1.Name);
            Assert.AreEqual(12, person1.Age);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
