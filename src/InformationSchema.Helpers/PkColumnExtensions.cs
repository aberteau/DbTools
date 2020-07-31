using System.Collections.Generic;
using System.Linq;
using DbTools.InformationSchema.Data;

namespace DbTools.InformationSchema.Helpers
{
    public static class PkColumnExtensions
    {
        public static IEnumerable<PkColumn> WhereBelongsTo(this IEnumerable<PkColumn> columns, string catalog, string schema, string name)
        {
            return columns.Where(c => c.TableCatalog.Equals(catalog) && c.TableSchema.Equals(schema) && c.TableName.Equals(name));   
        }

        public static IEnumerable<PkColumn> WhereBelongsTo(this IEnumerable<PkColumn> columns, Table tableDto)
        {
            return columns.WhereBelongsTo(tableDto.Catalog, tableDto.Schema, tableDto.Name);
        }
    }
}