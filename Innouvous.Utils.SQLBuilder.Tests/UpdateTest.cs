using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Framework.QueryModels;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Tests
{
    public static class UpdateTest
    {
        private static string table = "table";
        private static ColumnDefinition col1 = new ColumnDefinition(table, "Col1");
        private static ColumnDefinition col2 = new ColumnDefinition(table, "Col2");
        private static ColumnDefinition col3 = new ColumnDefinition(table, "Col3");
        private static ColumnDefinition col4 = new ColumnDefinition(table, "Col4");

        public static string TestUpdate(ISQLQueryWriter writer)
        {
            var query = new UpdateModel(table, 
                new List<WhereDefinition>()
                {
                    new WhereDefinition(col1,"=", 1),
                    new WhereDefinition(WhereType.And,col2,"=", "test")
                },
                new List<KeyValuePair<ColumnDefinition, object>>()
                {
                    new KeyValuePair<ColumnDefinition,object>(col3, 1),
                    new KeyValuePair<ColumnDefinition,object>(col4, "test2")
                });

            return writer.CreateUpdateQuery(query);
        }
    }
}
