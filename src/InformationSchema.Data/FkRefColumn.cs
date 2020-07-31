namespace DbTools.InformationSchema.Data
{
    public class FkRefColumn
    {
        public string ConstraintName { get; set; }

        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ReferencedTableCatalog { get; set; }

        public string ReferencedTableSchema { get; set; }

        public string ReferencedTableName { get; set; }

        public string ReferencedColumnName { get; set; }
    }
}