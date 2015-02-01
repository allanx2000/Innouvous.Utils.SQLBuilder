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
        private static FieldColumn col1 = new FieldColumn(table, "Col1");
        private static FieldColumn col2 = new FieldColumn(table, "Col2");
        private static FieldColumn col3 = new FieldColumn(table, "Col3");
        private static FieldColumn col4 = new FieldColumn(table, "Col4");

        public static string TestUpdate(ISQLQueryWriter writer)
        {
            var query = new UpdateModel(table, 
                new List<WhereDefinition>()
                {
                    new WhereDefinition(col1,"=", 1),
                    new WhereDefinition(WhereType.And,col2,"=", "test")
                },
                new List<KeyValuePair<FieldColumn, object>>()
                {
                    new KeyValuePair<FieldColumn,object>(col3, 1),
                    new KeyValuePair<FieldColumn,object>(col4, "test2")
                });

            return writer.CreateUpdateQuery(query);
        }
    }
}
