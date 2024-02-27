using System.Collections.Generic;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class Table
    {
        [JsonProperty(PropertyName = "identifier")]
        public TableIdentifier Identifier { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public IEnumerable<Column> Columns { get; set; }

        [JsonProperty(PropertyName = "foreignKeyConstraints")]
        public IEnumerable<ForeignKeyConstraint> ForeignKeyConstraints { get; set; }
    }
}