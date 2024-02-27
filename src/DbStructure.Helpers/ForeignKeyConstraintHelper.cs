using DbTools.DbStructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace DbTools.DbStructure.Helpers
{
    public class ForeignKeyConstraintHelper
    {
        private static TableIdentifier GetTableIdentifier(InformationSchema.Data.FkRefColumn fkRefColumn)
        {
            TableIdentifier tableIdentifier = new TableIdentifier();
            tableIdentifier.Catalog = fkRefColumn.ReferencedTableCatalog;
            tableIdentifier.Schema = fkRefColumn.ReferencedTableSchema;
            tableIdentifier.Name = fkRefColumn.ReferencedTableName;
            return tableIdentifier;
        }

        public static IEnumerable<ForeignKeyConstraint> GetForeignKeyConstraints(IEnumerable<InformationSchema.Data.FkRefColumn> fkRefColumns)
        {
            IEnumerable<IGrouping<string, InformationSchema.Data.FkRefColumn>> frRefGroups = fkRefColumns.GroupBy(f => f.ConstraintName);
            IEnumerable<ForeignKeyConstraint> fkConstraints = frRefGroups.Select(c => ToForeignKeyConstraint(c)).ToList();
            return fkConstraints;
        }

        private static ForeignKeyConstraint ToForeignKeyConstraint(IGrouping<string, InformationSchema.Data.FkRefColumn> fkRefColumnGroup)
        {
            ForeignKeyConstraint fkConstraint = new ForeignKeyConstraint();
            fkConstraint.Name = fkRefColumnGroup.Key;
            fkConstraint.ReferencedTable = GetTableIdentifier(fkRefColumnGroup.First());
            fkConstraint.ColumnRelationships = GetColumnRelationships(fkRefColumnGroup);
            return fkConstraint;
        }

        private static IEnumerable<ColumnRelationship> GetColumnRelationships(IEnumerable<InformationSchema.Data.FkRefColumn> fkRefColumns)
        {
            IEnumerable<ColumnRelationship> columnRelationships = fkRefColumns.Select(fkRefColumn => ToColumnRelationship(fkRefColumn)).ToList();
            return columnRelationships;
        }

        private static ColumnRelationship ToColumnRelationship(InformationSchema.Data.FkRefColumn fkRefColumn)
        {
            ColumnRelationship columnRelationship = new ColumnRelationship();
            columnRelationship.ReferencingColumn = fkRefColumn.ColumnName;
            columnRelationship.ReferencedColumn = fkRefColumn.ReferencedColumnName;
            return columnRelationship;
        }
    }
}
