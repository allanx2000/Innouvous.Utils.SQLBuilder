using Innouvous.Utils.SQLBuilder.Framework.QueryParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryModels
{
    public class DeleteModel
    {
        public List<WhereDefinition> Where { get; private set; }
        public string Table { get; private set; }

        public bool HasWhere
        {
            get
            {
                return Where != null;
            }
        }

        public DeleteModel(string table, List<WhereDefinition> wheres = null)
        {
            Table = table;
            Where = wheres;
        }
    }
}
