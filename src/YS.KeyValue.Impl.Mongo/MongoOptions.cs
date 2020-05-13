using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YS.Knife;

namespace YS.KeyValue.Impl.Mongo
{

    [OptionsClass]
    public class MongoOptions
    {
        [Required]
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        [Required]
        public string DataBase { get; set; } = "defaultdb";
    }
}
