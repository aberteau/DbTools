using System;

namespace DbTools.InformationSchema.Data
{
    public class Column
    {
        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public String TableName { get; set; }

        public String ColumnName { get; set; }

        public string DataType { get; set; }

        public string IsNullable { get; set; }

        public Nullable<Int64> CharacterMaximumLength { get; set; }
    }
}