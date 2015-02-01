using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Framework.QueryModels;
using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Writers
{
    public abstract class AbstractBaseWriter : ISQLQueryWriter
    {
        public string CreateSelectQuery(SelectModel model)
        {
            return CreateSelect(model);
        }

        protected abstract string CreateSelect(SelectModel model);

        
        public string CreateInsertQuery(InsertModel query)
        {
            string result;

            if (query.HasKeyValues)
                result = CreateInsert(query.Table, query.KeyValues);
            else
                result = CreateInsert(query.Table, query.Values);

            return result;
        }
        
        protected abstract string CreateInsert(string table, List<KeyValuePair<ColumnDefinition, object>> values);
        protected abstract string CreateInsert(string table, List<object> values);


        public string CreateUpdateQuery(UpdateModel query)
        {
            return CreateUpdate(query);
        }
        protected abstract string CreateUpdate(UpdateModel query);
        
        public string CreateDeleteQuery(DeleteModel query)
        {
            return CreateDelete(query);
        }
        protected abstract string CreateDelete(DeleteModel query);
        
    }
}
