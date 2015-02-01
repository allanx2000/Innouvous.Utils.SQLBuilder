using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Tests
{
    public class DeleteTest
    {
        private static string table = "table";
        private static FieldColumn col1 = new FieldColumn(table, "ID");
        public static string DeleteAll(ISQLQueryWriter writer)
        {
            var q = QueryModelFactory.CreateDeleteAll(table);

            return writer.CreateDeleteQuery(q);
        }

        public static string Delete(ISQLQueryWriter writer)
        {
            var q = QueryModelFactory.CreateDelete(table, new List<WhereDefinition>()
                {
                    new WhereDefinition(col1, "=", 1)
                });

            return writer.CreateDeleteQuery(q);
        }
    }
}
