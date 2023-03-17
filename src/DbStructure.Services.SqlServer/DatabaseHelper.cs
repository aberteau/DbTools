using System;
using System.Collections.Generic;
using System.Linq;
using DbTools.DbStructure.Data;
using DbTools.DbStructure.Helpers;
using Microsoft.Data.SqlClient;

namespace DbTools.DbStructure.Services.SqlServer
{
    public class DatabaseHelper
    {
        public static Data.Database GetDatabase(String connectionString, string tableCatalog, string tableSchema)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Data.Database database = GetDatabase(connection, tableCatalog, tableSchema);
            connection.Close();
            return database;
        }

        public static Data.Database GetDatabase(SqlConnection connection, string tableCatalog, string tableSchema)
        {
            Data.Database database = new Data.Database();
            database.Tables = GetTables(connection, tableCatalog, tableSchema);
            return database;
        }

        private static Data.Table GetTable(SqlConnection connection, IEnumerable<InformationSchema.Data.Column> columns, IEnumerable<InformationSchema.Data.PkColumn> pkColumns, InformationSchema.Data.Table table)
        {
            IEnumerable<InformationSchema.Data.FkRefColumn> fkRefColumns = InformationSchemaDao.GetFkColumns(connection, table.Catalog, table.Schema, table.Name);
            Data.TableIdentifier tableIdentifier = IdentifierHelper.GetTableIdentifier(table);
            Data.Table rTables = new Data.Table { Identifier = tableIdentifier };
            rTables.Columns = ColumnHelper.GetColumns(columns, pkColumns, fkRefColumns, table);
            return rTables;
        }

        private static IEnumerable<Data.Table> GetTables(SqlConnection connection, string tableCatalog, string tableSchema)
        {
            IEnumerable<InformationSchema.Data.Table> tables = InformationSchemaDao.GetTables(connection, tableCatalog, tableSchema);
            IEnumerable<InformationSchema.Data.Column> columns = InformationSchemaDao.GetColumns(connection, tableCatalog, tableSchema);
            IEnumerable<InformationSchema.Data.PkColumn> pkColumns = InformationSchemaDao.GetPkColumns(connection, tableCatalog, tableSchema);

            IEnumerable<Table> rTables = tables.Select(table => GetTable(connection, columns, pkColumns, table)).ToList();
            return rTables;
        }
    }
}