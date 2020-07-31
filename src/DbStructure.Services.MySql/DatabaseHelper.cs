using System;
using System.Collections.Generic;
using DbTools.DbStructure.Helpers;
using MySql.Data.MySqlClient;

namespace DbTools.DbStructure.Services.MySql
{
    public class DatabaseHelper
    {
        public static Data.Database GetDatabase(String connectionString, string tableCatalog, string tableSchema)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            Data.Database database = GetDatabase(connection, tableCatalog, tableSchema);
            connection.Close();
            return database;
        }

        public static Data.Database GetDatabase(MySqlConnection connection, string tableCatalog, string tableSchema)
        {
            Data.Database database = new Data.Database();
            database.Tables = GetTables(connection, tableCatalog, tableSchema);
            return database;
        }

        private static Data.Table GetTable(MySqlConnection connection, IEnumerable<InformationSchema.Data.Column> columns, IEnumerable<InformationSchema.Data.PkColumn> pkColumns, InformationSchema.Data.Table table)
        {
            IEnumerable<InformationSchema.Data.FkRefColumn> fkRefColumns = InformationSchemaDao.GetFkColumns(connection, table.Schema, table.Name);
            Data.TableIdentifier tableIdentifier = IdentifierHelper.GetTableIdentifier(table);
            Data.Table rTables = new Data.Table { Identifier = tableIdentifier };
            rTables.Columns = ColumnHelper.GetColumns(columns, pkColumns, fkRefColumns, table);
            return rTables;
        }

        private static IEnumerable<Data.Table> GetTables(MySqlConnection connection, string tableCatalog, string tableSchema)
        {
            IEnumerable<InformationSchema.Data.Table> tables = InformationSchemaDao.GetTables(connection, tableCatalog, tableSchema);
            IEnumerable<InformationSchema.Data.Column> columns = InformationSchemaDao.GetColumns(connection, tableCatalog, tableSchema);
            IEnumerable<InformationSchema.Data.PkColumn> pkColumns = InformationSchemaDao.GetPkColumns(connection, tableCatalog, tableSchema);

            IList<Data.Table> rTables = new List<Data.Table>();
            foreach (InformationSchema.Data.Table table in tables)
            {
                Data.Table rTable = GetTable(connection, columns, pkColumns, table);
                rTables.Add(rTable);
            }
            return rTables;
        }
    }
}