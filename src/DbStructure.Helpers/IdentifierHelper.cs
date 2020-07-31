using DbTools.DbStructure.Data;

namespace DbTools.DbStructure.Helpers
{
    public class IdentifierHelper
    {
        public static TableIdentifier GetTableIdentifier(InformationSchema.Data.Table tableRow)
        {
            TableIdentifier tableIdentifier = new TableIdentifier();
            tableIdentifier.Catalog = tableRow.Catalog;
            tableIdentifier.Schema = tableRow.Schema;
            tableIdentifier.Name = tableRow.Name;
            return tableIdentifier;
        }

        public static ColumnIdentifier GetFkReferencedColumnIdentifier(InformationSchema.Data.FkRefColumn fkColumnDto)
        {
            if (fkColumnDto == null)
                return null;

            ColumnIdentifier columnIdentifier = new ColumnIdentifier();
            columnIdentifier.TableIdentifier = GetTableIdentifier(fkColumnDto);
            columnIdentifier.ColumnName = fkColumnDto.ReferencedColumnName;
            return columnIdentifier;
        }

        private static TableIdentifier GetTableIdentifier(InformationSchema.Data.FkRefColumn fkColumnDto)
        {
            TableIdentifier tableIdentifier = new TableIdentifier();
            tableIdentifier.Catalog = fkColumnDto.ReferencedTableCatalog;
            tableIdentifier.Schema = fkColumnDto.ReferencedTableSchema;
            tableIdentifier.Name = fkColumnDto.ReferencedTableName;
            return tableIdentifier;
        }
    }
}