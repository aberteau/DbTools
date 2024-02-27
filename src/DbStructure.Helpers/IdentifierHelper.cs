using DbTools.DbStructure.Data;

namespace DbTools.DbStructure.Helpers
{
    public class IdentifierHelper
    {
        public static TableIdentifier GetTableIdentifier(InformationSchema.Data.Table table)
        {
            TableIdentifier tableIdentifier = new TableIdentifier();
            tableIdentifier.Catalog = table.Catalog;
            tableIdentifier.Schema = table.Schema;
            tableIdentifier.Name = table.Name;
            return tableIdentifier;
        }
    }
}