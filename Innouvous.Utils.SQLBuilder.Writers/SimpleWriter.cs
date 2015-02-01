using Innouvous.Utils.SQLBuilder.Framework.QueryModels;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Writers
{
    /// <summary>
    /// Generic SQL (MSSQL, Sybase, MySQL)
    /// </summary>
    public class SimpleWriter : AbstractBaseWriter
    {
        #region Private

        
        private string MakeWhereCondition(WhereDefinition where, bool includeTableName)
        {
            string name = MakeColumnName(where.Column, includeTableName);

            return String.Join(" ", name, where.Operator, QueryFormat(where.Value));
        }

        private string MakeColumnName(IColumn column, bool includeTableName = true)
        {
            if (column is FieldColumn)
            {
                var col = (FieldColumn)column;

                return includeTableName? MakeFullName(col) : col.Field;
            }
            else if (column is ComplexColumn)
            {
                var col = (ComplexColumn)column;

                if (String.IsNullOrEmpty(col.Alias))
                    throw new Exception("Alias must be defined!");
                else
                    return col.Alias;
            }
            else throw new NotSupportedException();
        }
        
        private string MakeFullName(FieldColumn column)
        {
            string name = String.Format("{0}.{1}", column.Table, column.Field);

            return name;
        }

        private string CreateJoin(ICollection<JoinDefinition> joins)
        {
            StringBuilder join = new StringBuilder();

            foreach (var j in joins)
            {
                switch (j.Type)
                {
                    case JoinDefinition.JoinType.INNER:
                        join.Append("JOIN ");
                        break;
                    case JoinDefinition.JoinType.OUTER:
                        join.Append("LEFT JOIN ");
                        break;
                    default:
                        throw new Exception("Join Type not supported");
                }

                join.Append(j.JoinTable + " ON ");

                List<string> conditions = new List<string>();

                string currentCondition = "";

                for (int i = 0; i < j.Columns.Count; i++)
                {
                    bool isSecond = i % 2 == 1;

                    currentCondition += MakeColumnName(j.Columns[i]) + (isSecond ? "" : " = ");

                    if (isSecond)
                    {
                        conditions.Add(currentCondition);
                        currentCondition = "";
                    }
                }

                join.AppendLine(String.Join(" AND ", conditions));
            }

            return join.ToString().Trim();
        }

        private string QueryFormat(object value)
        {
            if (value is string)
                return "\"" + value + "\"";
            else
                return value.ToString();
        }

        private string CreateSelect(ICollection<IColumn> columns)
        {
            string selectString = "SELECT ";

            List<string> items = new List<string>();

            foreach (var c in columns)
            {
                string name;
                string alias;

                if (c is FieldColumn)
                {
                    var col = (FieldColumn) c;

                    name = MakeFullName(col);
                    alias = col.Alias;
                }
                else if (c is ComplexColumn)
                {
                    var col = (ComplexColumn) c;

                    name = col.Formula;
                    alias = col.Alias;
                }
                else throw new NotSupportedException();

                string fullName = name;
                if (!String.IsNullOrEmpty(alias))
                    fullName += " AS " + alias;

                items.Add(fullName);
            }

            selectString += String.Join(", ", items);

            return selectString;
        }

        private string CreateFrom(string table)
        {
            return "FROM " + table;

        }

        private string CreateWhere(ICollection<WhereDefinition> wheres)
        {
            return CreateWhere(wheres, true);
        }

        private string CreateWhere(ICollection<WhereDefinition> wheres, bool hasTableName)
        {
            StringBuilder whereBuilder = new StringBuilder("WHERE ");

            foreach (var where in wheres)
            {
                string joiner;

                switch (where.Joiner)
                {
                    case WhereType.Or:
                        joiner = " OR ";
                        break;
                    case WhereType.And:
                        joiner = " AND ";
                        break;
                    case WhereType.NA:
                        joiner = "";
                        break;
                    default:
                        throw new Exception("Invalid Condition");
                }

                whereBuilder.Append(joiner);
                whereBuilder.Append(MakeWhereCondition(where, hasTableName));
            }

            return whereBuilder.ToString();

        }

        private string CreateOrderBy(List<OrderByDefinition> orderby)
        {
            List<string> parts = new List<string>();

            foreach (var order in orderby)
            {
                string orderString;

                switch (order.Order)
                {
                    case OrderByDefinition.SortDirection.Ascending:
                        orderString = "ASC";
                        break;
                    case OrderByDefinition.SortDirection.Descending:
                        orderString = "DESC";
                        break;
                    default:
                        throw new Exception("Order not supported");
                }



                parts.Add(String.Format("{0} {1}", MakeColumnName(order.Column), orderString));
            }

            return "ORDER BY " + String.Join(", ", parts);
        }
        
        private string CreateGroupBy(List<IColumn> columns)
        {
            List<string> names = new List<string>();

            foreach (var c in columns)
            {

                if (c is FieldColumn)
                {
                    var col = (FieldColumn)c;

                    names.Add(MakeFullName(col));
                }
                else if (c is ComplexColumn)
                {
                    var col = (ComplexColumn)c;

                    if (String.IsNullOrEmpty(col.Alias))
                        throw new Exception("Alias must be defined to be used in WHERE");
                    else
                        names.Add(col.Alias);
                }
                else throw new NotSupportedException();
            }

            string groupBy = "GROUP BY " + String.Join(", ", names);

            return groupBy;
        }


        #endregion

        #region Implementations

        protected override string CreateSelect(SelectModel model)
        {
            StringBuilder sb = new StringBuilder();

            //SELECT
            string select;

            select = CreateSelect(model.GetSelect());

            sb.AppendLine(select);

            //FROM
            sb.AppendLine(CreateFrom(model.GetFrom()));

            //JOIN
            if (model.HasJoin)
                sb.AppendLine(CreateJoin(model.GetJoin()));

            //WHERE
            if (model.HasWhere)
                sb.AppendLine(CreateWhere(model.GetWhere()));

            //ORDER BY
            if (model.HasOrderBy)
                sb.AppendLine(CreateOrderBy(model.GetOrderBy()));

            //GROUP BY
            if (model.HasGroupBy)
                sb.AppendLine(CreateGroupBy(model.GetGroupBy()));

            return sb.ToString().Trim();
        }

        protected override string CreateInsert(string table, List<KeyValuePair<FieldColumn, object>> values)
        {
            string format = "INSERT INTO {0}({1}) VALUES({2})";

            string keys = String.Join(", ", from kv in values select kv.Key.Field);
            string data = String.Join(", ", from kv in values select QueryFormat(kv.Value));

            return String.Format(format, table, keys, data);
        }

        protected override string CreateInsert(string table, List<object> values)
        {
            string data = String.Join(", ", from v in values select QueryFormat(v));

            return string.Format("INSERT INTO {0} VALUES({1})", table, data);
        }

        protected override string CreateUpdate(UpdateModel query)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("UPDATE " + query.Table);

            string set = String.Join(", ", from kv in query.Set select kv.Key.Field + " = " + QueryFormat(kv.Value));
            sb.AppendLine("SET " + set);

            string where = CreateWhere(query.Where, false);

            sb.AppendLine(where);

            return sb.ToString().Trim();
        }

        protected override string CreateDelete(DeleteModel query)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM " + query.Table);

            if (query.HasWhere)
            {
                sb.Append(CreateWhere(query.Where, false));
            }

            return sb.ToString();
        }

        #endregion
    }
}
