using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class ColumnRelationship
    {
        [JsonProperty(PropertyName = "referencingColumn")]
        public string ReferencingColumn { get; set; }

        [JsonProperty(PropertyName = "referencedColumn")]
        public string ReferencedColumn { get; set; }
    }
}