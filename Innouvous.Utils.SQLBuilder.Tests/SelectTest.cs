using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using Innouvous.Utils.SQLBuilder.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Tests
{
    class SelectTest
    {
        private static readonly FieldColumn col1 = new FieldColumn(table1, "ID", "Table1_ID");
        private static readonly FieldColumn col2 = new FieldColumn(table1, "ID2");
        private static readonly FieldColumn col3 = new FieldColumn(table1, "ID3");

        private static readonly FieldColumn col4 = new FieldColumn(table2, "ID");
        private static readonly FieldColumn col5 = new FieldColumn(table2, "ID3");

        private static readonly FieldColumn col6 = new FieldColumn(table3, "ID");
        private static readonly ComplexColumn count = new ComplexColumn("count(*)", "cnt");

        private const string table1 = "table1";
        private const string table2 = "table2";
        private const string table3 = "table3";

        public static string TestComplexSelect(ISQLQueryWriter writer)
        {
            var query = QueryModelFactory.CreateSelect(col1, col4, col6,count)
                .From(table1)
                .Where(
                    new WhereDefinition(col1, "=", 1),
                    new WhereDefinition(WhereType.And, col4, "=", 2),
                    new WhereDefinition(WhereType.Or, count, ">", 3)
                )
                .Join(
                    new JoinDefinition(JoinDefinition.JoinType.INNER, table2, col1, col3),  
                    new JoinDefinition(JoinDefinition.JoinType.INNER, table3, col2, col4, col3, col6)
                )
                .OrderBy(
                    new OrderByDefinition(count),
                    new OrderByDefinition(col4, OrderByDefinition.SortDirection.Descending)
                )
                .GroupBy(col1,col4);

            return writer.CreateSelectQuery(query);
        }

        public static string TestCustomSelectString(ISQLQueryWriter writer)
        {
            var query = QueryModelFactory.CreateSelect(new ComplexColumn("count(*)"))
                .From(table1);

            return writer.CreateSelectQuery(query);
        }

    }
}
