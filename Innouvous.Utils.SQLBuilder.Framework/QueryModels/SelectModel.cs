using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryModels
{
    public class SelectModel
    {
        private List<IColumn> select;
        private string from;
        private List<WhereDefinition> wheres;
        private List<JoinDefinition> joins;
        private List<OrderByDefinition> orderby;
        private List<IColumn> groupBy;

        public bool HasGroupBy
        {
            get
            {
                return HasValue(groupBy);
            }
        }

        public bool HasWhere
        {
            get
            {
                return HasValue(wheres);
            }
        }

        public bool HasJoin
        {
            get
            {
                return HasValue(joins);
            }
        }

        public bool HasOrderBy
        {
            get
            {
                return HasValue(orderby);
            }
        }

        private bool HasValue(IList list)
        {
            return list != null && list.Count > 0;
        }

        public SelectModel(List<IColumn> selectColumns)
        {
            this.select = selectColumns;
        }

        #region Getters

        public List<IColumn> GetSelect()
        {
            return new List<IColumn>(select);
        }

        public string GetFrom()
        {
            return from;
        }

        public List<WhereDefinition> GetWhere()
        {
            return HasWhere ? new List<WhereDefinition>(wheres) : null;
        }

        public List<JoinDefinition> GetJoin()
        {
            return HasJoin ? new List<JoinDefinition>(joins) : null;
        }

        
        public List<OrderByDefinition> GetOrderBy()
        {
            return HasOrderBy ? new List<OrderByDefinition>(orderby) : null;
        }

        public List<IColumn> GetGroupBy()
        {
            return HasGroupBy ? new List<IColumn>(groupBy) : null;
        }


        #endregion

        public SelectModel Select(params IColumn[] columns)
        {
            if (select != null)
                throw new Exception("Select has already been defined!");

            select = new List<IColumn>();

            foreach (IColumn col in columns)
            {
                select.Add(col);
            }

            return this;
        }

        public SelectModel From(string table)
        {
            if (from != null)
                throw new Exception("From has already been defined!");

            from = table;

            return this;
        }

        public SelectModel Where(params WhereDefinition[] wheres)
        {
            if (this.wheres != null)
                throw new Exception("Where clause already exists");
            else if (wheres.Count(x => x.Joiner == WhereType.NA) > 2)
                throw new Exception("Only 1 condition can be NA");
            else
            {
                this.wheres = wheres.ToList();
            }

            return this;
        }

        public SelectModel Join(params JoinDefinition[] joins)
        {
            if (this.joins != null)
                throw new Exception("Join already declared");
            else
                this.joins = joins.ToList();

            return this;
        }
        
        public SelectModel OrderBy(params OrderByDefinition[] order)
        {
            if (this.orderby != null)
                throw new Exception("OrderBy already declared");
            else
                this.orderby = order.ToList();

            return this;
        }



        public SelectModel GroupBy(params IColumn[] columns)
        {
            if (this.groupBy != null)
                throw new Exception("GroupBy already declared");
            else
                this.groupBy = columns.ToList();


            return this;
        }
    }
}
