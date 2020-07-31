using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DbTools.Core.MySql
{
    public class MySqlCommandHelper
    {
        public static IEnumerable<T> GetRows<T>(MySqlCommand command, Func<MySqlDataReader, T> mapToRow)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                IEnumerable<T> rows = reader.ReadAll(r => mapToRow(reader));
                return rows;
            }
        }
    }
}
