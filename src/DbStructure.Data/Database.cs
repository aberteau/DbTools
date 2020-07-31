using System.Collections.Generic;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class Database
    {
        [JsonProperty(PropertyName = "tables")]
        public IEnumerable<Table> Tables { get; set; }
    }
}