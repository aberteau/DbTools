using System;
using System.Data;

namespace DbTools.Core
{
    public static class DataRecordExtensions
    {
        public static String SafeGetString(this IDataRecord dataRecord, int colIndex)
        {
            if (dataRecord.IsDBNull(colIndex))
                return null;

            string str = dataRecord.GetString(colIndex);
            return str;
        }

        public static String GetString(this IDataRecord dataRecord, string columnName)
        {
            Int32 columnOrdinal = dataRecord.GetOrdinal(columnName);
            return dataRecord.SafeGetString(columnOrdinal);
        }

        #region Nullable<Int32>

        public static Nullable<Int32> GetNullableInt32(this IDataRecord dataRecord, int colIndex)
        {
            if (!dataRecord.IsDBNull(colIndex))
                return dataRecord.GetInt32(colIndex);

            return null;
        }

        public static Nullable<Int32> GetNullableInt32(this IDataRecord dataRecord, string columnName)
        {
            Int32 columnOrdinal = dataRecord.GetOrdinal(columnName);
            return dataRecord.GetNullableInt32(columnOrdinal);
        }

        #endregion

        #region Nullable<Int64>

        public static Nullable<Int64> GetNullableInt64(this IDataRecord dataRecord, int colIndex)
        {
            if (!dataRecord.IsDBNull(colIndex))
                return dataRecord.GetInt64(colIndex);

            return null;
        }

        public static Nullable<Int64> GetNullableInt64(this IDataRecord dataRecord, string columnName)
        {
            Int32 columnOrdinal = dataRecord.GetOrdinal(columnName);
            return dataRecord.GetNullableInt64(columnOrdinal);
        }

        #endregion
    }
}