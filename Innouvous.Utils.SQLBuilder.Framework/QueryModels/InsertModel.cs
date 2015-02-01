using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryModels
{
    public class InsertModel
    {
        public string Table { get; private set; }
        public List<object> Values { get; private set; }
        public List<KeyValuePair<FieldColumn, object>> KeyValues { get; private set; }
        
        public InsertModel(string table, object[] values)
        {
            Table = table;
            Values = values.ToList();
        }

        public InsertModel(string table, List<KeyValuePair<FieldColumn, object>> values)
        {
            Table = table;
            KeyValues = values;
        }
        
        public bool HasKeyValues
        {
            get
            {
                return KeyValues != null;
            }
        }
    }
}
