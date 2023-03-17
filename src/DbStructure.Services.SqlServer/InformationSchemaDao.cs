using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbTools.Core;
using DbTools.InformationSchema.Data;
using DbTools.InformationSchema.Helpers;
using Microsoft.Data.SqlClient;

namespace DbTools.DbStructure.Services.SqlServer
{
    public class InformationSchemaDao
    {
        #region Table

        public static IEnumerable<Table> GetTables(SqlConnection connection, String tableCatalog, String tableSchema)
        {
            IList<Table> tables = GetTables(connection, tableCatalog, tableSchema, TableType.BaseTable).ToList();
            return tables;
        }

        public static IEnumerable<Table> GetTables(SqlConnection connection, String tableCatalog, String tableSchema, Nullable<TableType> tableType)
        {
            StringBuilder cmdTextStringBuilder = new StringBuilder();
            cmdTextStringBuilder.Append(@"SELECT
                                table_catalog,
                                table_schema,
                                table_name,
                                table_type
                            FROM information_schema.tables");

            bool isTableCatalogNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableCatalog);
            bool isTableSchemaNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableSchema);
            bool isTableTypeNull = !tableType.HasValue;

            if (!isTableCatalogNullOrWhiteSpace || !isTableSchemaNullOrWhiteSpace || !isTableTypeNull)
            {
                StringBuilder conditionsTextStringBuilder = new StringBuilder();

                if (!isTableCatalogNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("table_catalog = @tableCatalog");

                if (!isTableSchemaNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("table_schema = @tableSchema");

                if (!isTableTypeNull)
                    conditionsTextStringBuilder.AppendSqlCondition("table_type = @tableType");

                cmdTextStringBuilder.Append(" WHERE ");
                cmdTextStringBuilder.Append(conditionsTextStringBuilder);
            }

            string cmdText = cmdTextStringBuilder.ToString();
            SqlCommand command = new SqlCommand(cmdText, connection);

            if (!isTableCatalogNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableCatalog", tableCatalog);

            if (!isTableSchemaNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableSchema", tableSchema);

            if (!isTableTypeNull)
                command.Parameters.AddWithValue("@tableType", TableTypeHelper.GetTableTypeStr(tableType));

            IEnumerable<Table> tables = command.ExecuteQuery(r => ToTable(r));
            return tables;
        }

        private static Table ToTable(IDataRecord reader)
        {
            Table table = new Table();
            table.Catalog = reader.GetString("table_catalog");
            table.Schema = reader.GetString("table_schema");
            table.Name = reader.GetString("table_name");
            string tableTypeStr = reader.GetString("table_type");
            table.Type = TableTypeHelper.ToTableType(tableTypeStr);
            return table;
        }

        #endregion

        #region Column

        public static IEnumerable<Column> GetColumns(SqlConnection connection, String tableCatalog, String tableSchema)
        {
            StringBuilder cmdTextStringBuilder = new StringBuilder();
            cmdTextStringBuilder.Append(@"SELECT
                            table_catalog,
                            table_schema,
                            table_name,
                            column_name,
                            data_type,
                            character_maximum_length,
                            is_nullable
                        FROM information_schema.columns");

            bool isTableCatalogNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableCatalog);
            bool isTableSchemaNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableSchema);

            if (!isTableCatalogNullOrWhiteSpace || !isTableSchemaNullOrWhiteSpace)
            {
                StringBuilder conditionsTextStringBuilder = new StringBuilder();

                if (!isTableCatalogNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("table_catalog = @tableCatalog");

                if (!isTableSchemaNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("table_schema = @tableSchema");

                cmdTextStringBuilder.Append(" WHERE ");
                cmdTextStringBuilder.Append(conditionsTextStringBuilder);
            }

            string cmdText = cmdTextStringBuilder.ToString();
            SqlCommand command = new SqlCommand(cmdText, connection);

            if (!isTableCatalogNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableCatalog", tableCatalog);

            if (!isTableSchemaNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableSchema", tableSchema);

            IEnumerable<Column> columns = command.ExecuteQuery(r => ToColumn(r));
            return columns;
        }

        private static Column ToColumn(IDataRecord reader)
        {
            Column column = new Column();
            column.TableCatalog = reader.GetString("table_catalog");
            column.TableSchema = reader.GetString("table_schema");
            column.TableName = reader.GetString("table_name");
            column.ColumnName = reader.GetString("column_name");
            column.DataType = reader.GetString("data_type");
            column.CharacterMaximumLength = reader.GetNullableInt32("character_maximum_length");
            column.IsNullable = reader.GetString("is_nullable");
            return column;
        }

        #endregion

        #region PkColumn

        public static IEnumerable<PkColumn> GetPkColumns(SqlConnection connection, String tableCatalog, String tableSchema)
        {
            IList<PkColumn> dtos = new List<PkColumn>();
            StringBuilder cmdTextStringBuilder = new StringBuilder();
            cmdTextStringBuilder.Append(@"SELECT
                                kcu.table_catalog,
                                kcu.table_schema,
                                kcu.table_name,
                                kcu.column_name
                            FROM information_schema.table_constraints tc
                            INNER JOIN information_schema.key_column_usage kcu
                                ON kcu.constraint_catalog = tc.constraint_catalog
                                AND kcu.constraint_schema = tc.constraint_schema                                
                                AND kcu.constraint_name = tc.constraint_name
                            WHERE tc.constraint_type ='PRIMARY KEY'");

            bool isTableCatalogNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableCatalog);
            bool isTableSchemaNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableSchema);

            if (!isTableCatalogNullOrWhiteSpace || !isTableSchemaNullOrWhiteSpace)
            {
                StringBuilder conditionsTextStringBuilder = new StringBuilder();

                if (!isTableCatalogNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("tc.constraint_catalog = @tableCatalog");

                if (!isTableSchemaNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("tc.constraint_schema = @tableSchema");

                cmdTextStringBuilder.Append(" AND ");
                cmdTextStringBuilder.Append(conditionsTextStringBuilder);
            }

            string cmdText = cmdTextStringBuilder.ToString();
            SqlCommand command = new SqlCommand(cmdText, connection);

            if (!isTableCatalogNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableCatalog", tableCatalog);

            if (!isTableSchemaNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableSchema", tableSchema);

            IEnumerable<PkColumn> pkColumns = command.ExecuteQuery(r => ToPkColumn(r));
            return pkColumns;
        }

        private static PkColumn ToPkColumn(IDataRecord reader)
        {
            PkColumn pkColumn = new PkColumn();
            pkColumn.TableCatalog = reader.GetString("table_catalog");
            pkColumn.TableSchema = reader.GetString("table_schema");
            pkColumn.TableName = reader.GetString("table_name");
            pkColumn.ColumnName = reader.GetString("column_name");
            return pkColumn;
        }

        #endregion

        #region FkRefColumn

        public static IEnumerable<FkRefColumn> GetFkColumns(SqlConnection connection, string tableCatalog, string tableSchema, string tableName)
        {
            StringBuilder cmdTextStringBuilder = new StringBuilder();
            cmdTextStringBuilder.Append(@"SELECT
                    tc.CONSTRAINT_NAME AS constraint_name,
                    kcu.TABLE_CATALOG AS table_catalog,
                    kcu.TABLE_SCHEMA AS table_schema,
                    kcu.TABLE_NAME AS table_name,
                    kcu.COLUMN_NAME AS column_name,
                    kcu2.TABLE_CATALOG AS referenced_table_catalog,
                    kcu2.TABLE_SCHEMA AS referenced_table_schema,
                    kcu2.TABLE_NAME AS referenced_table_name,
                    kcu2.COLUMN_NAME AS referenced_column_name
                    FROM information_schema.table_constraints AS tc
                    JOIN information_schema.key_column_usage AS kcu
	                    ON kcu.CONSTRAINT_CATALOG = tc.CONSTRAINT_CATALOG
	                    AND kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA
	                    AND kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
                    JOIN information_schema.referential_constraints AS rc
	                    ON rc.CONSTRAINT_CATALOG = kcu.CONSTRAINT_CATALOG
	                    AND rc.CONSTRAINT_SCHEMA = kcu.CONSTRAINT_SCHEMA
	                    AND rc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                    JOIN information_schema.key_column_usage AS kcu2
	                    ON kcu2.CONSTRAINT_CATALOG = rc.UNIQUE_CONSTRAINT_CATALOG
	                    AND kcu2.CONSTRAINT_SCHEMA = rc.UNIQUE_CONSTRAINT_SCHEMA
	                    AND kcu2.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME
                    WHERE tc.constraint_type = 'FOREIGN KEY'");

            bool isTableCatalogNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableCatalog);
            bool isTableSchemaNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableSchema);
            bool isTableNameNullOrWhiteSpace = String.IsNullOrWhiteSpace(tableName);

            if (!isTableCatalogNullOrWhiteSpace || !isTableSchemaNullOrWhiteSpace || !isTableNameNullOrWhiteSpace)
            {
                StringBuilder conditionsTextStringBuilder = new StringBuilder();

                if (!isTableCatalogNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("tc.TABLE_CATALOG = @tableCatalog");

                if (!isTableSchemaNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("tc.TABLE_SCHEMA = @tableSchema");

                if (!isTableNameNullOrWhiteSpace)
                    conditionsTextStringBuilder.AppendSqlCondition("tc.TABLE_NAME = @tableName");

                cmdTextStringBuilder.Append(" AND ");
                cmdTextStringBuilder.Append(conditionsTextStringBuilder);
            }

            string cmdText = cmdTextStringBuilder.ToString();
            SqlCommand command = new SqlCommand(cmdText, connection);

            if (!isTableCatalogNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableCatalog", tableCatalog);

            if (!isTableSchemaNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableSchema", tableSchema);

            if (!isTableNameNullOrWhiteSpace)
                command.Parameters.AddWithValue("@tableName", tableName);

            IEnumerable<FkRefColumn> fkRefColumns = command.ExecuteQuery(r => ToFkRefColumn(r));
            return fkRefColumns;
        }

        private static FkRefColumn ToFkRefColumn(IDataRecord reader)
        {
            FkRefColumn fkRefColumn = new FkRefColumn();
            fkRefColumn.ConstraintName = reader.GetString("constraint_name");
            fkRefColumn.TableCatalog = reader.GetString("table_catalog");
            fkRefColumn.TableSchema = reader.GetString("table_schema");
            fkRefColumn.TableName = reader.GetString("table_name");
            fkRefColumn.ColumnName = reader.GetString("column_name");
            fkRefColumn.ReferencedTableCatalog = reader.GetString("referenced_table_catalog");
            fkRefColumn.ReferencedTableSchema = reader.GetString("referenced_table_schema");
            fkRefColumn.ReferencedTableName = reader.GetString("referenced_table_name");
            fkRefColumn.ReferencedColumnName = reader.GetString("referenced_column_name");
            return fkRefColumn;
        }

        #endregion
    }
}