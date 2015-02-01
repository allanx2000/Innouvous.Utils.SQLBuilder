using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Tests
{
    public class InsertTest
    {
        private static Dictionary<FieldColumn, object> values = new Dictionary<FieldColumn, object>()
        {
            {new FieldColumn(table,"ID"), 1},
            {new FieldColumn(table,"Name"), "test"}
        };

        private const string table = "table1";

        public static string TestInsertWithKeys(ISQLQueryWriter writer)
        {
            var query = QueryModelFactory.CreateInsert(table, values.ToList());

            return writer.CreateInsertQuery(query);
        }


        public static string TestInsert(ISQLQueryWriter writer)
        {
            var query = QueryModelFactory.CreateInsert(table, values.Values.ToList());

            return writer.CreateInsertQuery(query);
        }
    }
}
