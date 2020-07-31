using System;
using Newtonsoft.Json;

namespace DbTools.DbStructure.Data
{
    public class TableIdentifier
    {
        [JsonProperty(PropertyName = "catalog")]
        public string Catalog { get; set; }

        [JsonProperty(PropertyName = "schema")]
        public string Schema { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        public override bool Equals(object obj)
        {
            TableIdentifier tableIdentifier = obj as TableIdentifier;
            if (tableIdentifier == null)
                return false;

            return (Catalog, Schema, Name).Equals((tableIdentifier.Catalog, tableIdentifier.Schema, tableIdentifier.Name));
        }

        public override int GetHashCode()
        {
            return (Catalog, Schema, Name).GetHashCode();
        }
    }
}