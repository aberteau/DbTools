using System.Collections.Generic;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class ForeignKeyConstraint
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "referencedTable")]
        public TableIdentifier ReferencedTable { get; set; }

        [JsonProperty(PropertyName = "columnRelationships")]
        public IEnumerable<ColumnRelationship> ColumnRelationships { get; set; }
    }
}