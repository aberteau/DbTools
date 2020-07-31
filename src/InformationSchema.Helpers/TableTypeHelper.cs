using DbTools.InformationSchema.Data;

namespace DbTools.InformationSchema.Helpers
{
    public class TableTypeHelper
    {
        public static string GetTableTypeStr(TableType? tableType)
        {
            if (!tableType.HasValue)
                return null;

            string typeStr = GetTableTypeStr(tableType.Value);
            return typeStr;
        }

        public static string GetTableTypeStr(TableType tableType)
        {
            switch (tableType)
            {
                case TableType.BaseTable:
                    return "BASE TABLE";

                case TableType.View:
                    return "VIEW";
            }

            return null;
        }

        public static TableType ToTableType(string tableType)
        {
            if (tableType.Equals("VIEW"))
                return TableType.View;

            return TableType.BaseTable;
        }
    }
}
