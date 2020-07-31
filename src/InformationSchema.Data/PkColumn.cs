using System;

namespace DbTools.InformationSchema.Data
{
    public class PkColumn
    {
        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public String TableName { get; set; }

        public String ColumnName { get; set; }
    }
}