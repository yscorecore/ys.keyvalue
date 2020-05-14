using YS.Knife;
using System.ComponentModel.DataAnnotations;

namespace YS.KeyValue.Impl.Dynamo
{
    [OptionsClass]
    public class DynamoOptions
    {
        [Url]
        public string ServiceUrl { get; set; } = "http://localhost:8000/";

        public string AwsAccessKeyId { get; set; } = "";
        public string AwsSecretAccessKey { get; set; } = "";
    }
}
