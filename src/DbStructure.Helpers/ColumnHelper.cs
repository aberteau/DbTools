using System.Collections.Generic;
using System.Linq;
using DbTools.InformationSchema.Data;
using DbTools.InformationSchema.Helpers;

namespace DbTools.DbStructure.Helpers
{
    public class ColumnHelper
    {
        public static IEnumerable<Data.Column> GetColumns(IEnumerable<Column> columns, IEnumerable<PkColumn> pkColumns, IEnumerable<FkRefColumn> fkRefColumns, Table table)
        {
            IEnumerable<Column> tableColumns = columns.WhereBelongsTo(table);
            IEnumerable<PkColumn> tablePkColumns = pkColumns.WhereBelongsTo(table).ToList();

            IList<FkRefColumn> fkRefColumnList = fkRefColumns.ToList();

            IList<Data.Column> columnList = new List<Data.Column>();

            foreach (Column tableColumn in tableColumns)
            {
                Data.Column column = new Data.Column();
                column.Name = tableColumn.ColumnName;
                column.DataType = tableColumn.DataType;
                column.CharacterMaximumLength = tableColumn.CharacterMaximumLength;
                column.IsNullable = tableColumn.IsNullable.Equals("YES");

                column.IsPrimaryKey = tablePkColumns.Any(c => c.ColumnName.Equals(tableColumn.ColumnName));

                FkRefColumn fkRefColumn = fkRefColumnList.FirstOrDefault(c => c.ColumnName.Equals(tableColumn.ColumnName));
                column.FkRefColumnIdentifier = IdentifierHelper.GetFkReferencedColumnIdentifier(fkRefColumn);

                columnList.Add(column);
            }

            return columnList;
        }


    }
}