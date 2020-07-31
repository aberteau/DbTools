using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DbTools.Core.MySql
{
    public static class MySqlReaderExtensions
    {
        public static IEnumerable<T> ReadAll<T>(this MySqlDataReader reader, Func<MySqlDataReader, T> mapToRow)
        {
            IList<T> rows = new List<T>();
            while (reader.Read())
            {
                T row = mapToRow(reader);
                rows.Add(row);
            }
            return rows;
        }
    }
}
