using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryModels
{
    public class UpdateModel
    {
        public List<WhereDefinition> Where { get; private set; }
        public List<KeyValuePair<FieldColumn, object>> Set { get; private set; }

        public string Table {get; private set; }

        public UpdateModel(string table, List<WhereDefinition> where, List<KeyValuePair<FieldColumn, object>> set)
        {
            Table = table;
            Where = where;
            Set = set;
        }

    }
}
