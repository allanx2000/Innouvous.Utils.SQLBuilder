using Innouvous.Utils.SQLBuilder.Framework.QueryModels;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder
{
    public static class QueryModelFactory
    {
        public static SelectModel CreateSelect(string select)
        {
            return new SelectModel(select);
        }

        public static SelectModel CreateSelect(params ColumnDefinition[] columns)
        {
            return new SelectModel(columns.ToList());
        }

        public static SelectModel CreateSelect(List<ColumnDefinition> columns)
        {
            return new SelectModel(columns);
        }

        public static InsertModel CreateInsert(string table, List<object> values)
        {
            return new InsertModel(table, values.ToArray());
        }

        public static InsertModel CreateInsert(string table, params object[]  values)
        {
            return new InsertModel(table, values);
        }

        public static InsertModel CreateInsert(string table, params KeyValuePair<ColumnDefinition, object>[] values)
        {
            return CreateInsert(table, values.ToList());
        }

        public static InsertModel CreateInsert(string table, List<KeyValuePair<ColumnDefinition, object>> values)
        {
            return new InsertModel(table, values);
        }

        public static DeleteModel CreateDelete(string table, List<WhereDefinition> wheres)
        {
            if (wheres != null && wheres.Count > 0)
                return new DeleteModel(table, wheres);
            else
                throw new Exception("No WHERE conditions specified, if you want to delete all data, use CreateDeleteAll");
        }

        public static DeleteModel CreateDeleteAll(string table)
        {
            return new DeleteModel(table);
        }

        public static UpdateModel CreateUpdate(string table, List<KeyValuePair<ColumnDefinition,object>> sets, List<WhereDefinition> wheres)
        {
            return new UpdateModel(table, wheres, sets);
        }
    }
}
