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

        public static ColumnIdentifier GetFkReferencedColumnIdentifier(InformationSchema.Data.FkRefColumn fkRefColumn)
        {
            if (fkRefColumn == null)
                return null;

            ColumnIdentifier columnIdentifier = new ColumnIdentifier();
            columnIdentifier.TableIdentifier = GetTableIdentifier(fkRefColumn);
            columnIdentifier.ColumnName = fkRefColumn.ReferencedColumnName;
            return columnIdentifier;
        }

        private static TableIdentifier GetTableIdentifier(InformationSchema.Data.FkRefColumn fkRefColumn)
        {
            TableIdentifier tableIdentifier = new TableIdentifier();
            tableIdentifier.Catalog = fkRefColumn.ReferencedTableCatalog;
            tableIdentifier.Schema = fkRefColumn.ReferencedTableSchema;
            tableIdentifier.Name = fkRefColumn.ReferencedTableName;
            return tableIdentifier;
        }
    }
}