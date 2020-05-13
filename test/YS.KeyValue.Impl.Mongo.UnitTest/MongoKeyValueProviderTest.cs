using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Hosting;

namespace YS.KeyValue.Impl.Mongo.UnitTest
{
    [TestClass]
    public class MongoKeyValueProviderTest : KnifeHost
    {
        public MongoKeyValueProviderTest()
            : base(new System.Collections.Generic.Dictionary<string, object>
            {
                ["Mongo:ConnectionString"] = "mongodb://root:example@localhost:27017"
            })
        {

        }
        [TestMethod]
        public async Task TestMethod1()
        {
            var provider = this.GetService<IKeyValueProvider>();
            var data1 = await provider.GetByKey<Person>("testentity", "name1");
            Assert.IsNull(data1);
        }
        [TestMethod]
        public async Task ShouldSuccessWhenAddAValideEntity()
        {
            var provider = this.GetService<IKeyValueProvider>();
            await provider.AddOrUpdate("abc", "u0001", new Person() { Name = "zhangsan", Age = 12 });
            var person1 = await provider.GetByKey<Person>("abc", "u0001");
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
