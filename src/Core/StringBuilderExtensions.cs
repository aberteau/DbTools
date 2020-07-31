using System;
using System.Text;

namespace DbTools.Core
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSqlCondition(this StringBuilder stringBuilder, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                if (stringBuilder.Length != 0)
                    stringBuilder.Append(" AND ");

                stringBuilder.Append(value);
            }

            return stringBuilder;
        }
    }
}
