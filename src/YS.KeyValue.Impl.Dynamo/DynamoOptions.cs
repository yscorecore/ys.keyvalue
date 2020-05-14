using System;
using System.Collections.Generic;
using System.Text;
using YS.Knife;

namespace YS.KeyValue.Impl.Dynamo
{
    [OptionsClass]
    public class DynamoOptions
    {
        public string ServiceUrl { get; set; } = "http://localhost:8000/";

        public string AwsAccessKeyId { get; set; } = "";
        public string AwsSecretAccessKey { get; set; } = "";
    }
}
