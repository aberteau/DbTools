using System;

namespace DbTools.InformationSchema.Data
{
    public class Table
    {
        public string Catalog { get; set; }

        public string Schema { get; set; }

        public String Name { get; set; }

        public TableType Type { get; set; }
    }
}