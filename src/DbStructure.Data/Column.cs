using System;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class Column
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        [JsonProperty(PropertyName = "isNullable")]
        public bool IsNullable { get; set; }

        [JsonProperty(PropertyName = "isPrimaryKey")]
        public bool IsPrimaryKey { get; set; }

        [JsonProperty(PropertyName = "fkRefColumnIdentifier")]
        public ColumnIdentifier FkRefColumnIdentifier { get; set; }

        [JsonProperty(PropertyName = "characterMaximumLength")]
        public Nullable<Int64> CharacterMaximumLength { get; set; }
    }
}