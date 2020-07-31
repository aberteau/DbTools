using System.Collections.Generic;
using System.Linq;
using DbTools.InformationSchema.Data;

namespace DbTools.InformationSchema.Helpers
{
    public static class ColumnExtensions
    {
        private static bool BelongsTo(this Column column, string catalog, string schema, string name)
        {
            return column.TableCatalog.Equals(catalog) && column.TableSchema.Equals(schema) && column.TableName.Equals(name);
        }

        private static IEnumerable<Column> WhereBelongsTo(this IEnumerable<Column> columns, string catalog, string schema, string name)
        {
            return columns.Where(c => c.BelongsTo(catalog, schema, name));
        }

        public static IEnumerable<Column> WhereBelongsTo(this IEnumerable<Column> columns, Table tableDto)
        {
            return columns.WhereBelongsTo(tableDto.Catalog, tableDto.Schema, tableDto.Name);
        }
    }
}