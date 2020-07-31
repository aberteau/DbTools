using System;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class ColumnIdentifier
    {
        [JsonProperty(PropertyName = "tableIdentifier")]
        public TableIdentifier TableIdentifier { get; set; }

        [JsonProperty(PropertyName = "columnName")]
        public String ColumnName { get; set; }

        public override bool Equals(object obj)
        {
            ColumnIdentifier columnIdentifier = obj as ColumnIdentifier;
            if (columnIdentifier == null)
                return false;

            return (TableIdentifier, ColumnName).Equals((columnIdentifier.TableIdentifier, columnIdentifier.ColumnName));
        }

        public override int GetHashCode()
        {
            return (TableIdentifier, ColumnName).GetHashCode();
        }
    }
}
