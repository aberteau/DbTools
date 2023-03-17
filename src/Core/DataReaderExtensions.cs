using System;
using System.Collections.Generic;
using System.Data;

namespace DbTools.Core
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<T> ReadAll<T>(this IDataReader reader, Func<IDataRecord, T> map)
        {
            IList<T> rows = new List<T>();
            while (reader.Read())
            {
                T row = map(reader);
                rows.Add(row);
            }
            return rows;
        }
    }
}
