using System;
using System.Collections.Generic;
using System.Data;

namespace DbTools.Core
{
    public static class DbCommandExtensions
    {
        public static IEnumerable<T> GetAll<T>(this IDbCommand command, Func<IDataReader, T> map)
        {
            using (IDataReader reader = command.ExecuteReader())
            {
                IEnumerable<T> rows = reader.ReadAll(r => map(r));
                return rows;
            }
        }
    }
}
