using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public class FieldColumn : IColumn
    {
        public string Table { get; private set; }
        public string Field { get; private set; }

        public FieldColumn(string table, string fieldName, string alias = null)
        {
            this.Table = table;
            this.Field = fieldName;
            this.Alias = alias;
        }

        public string Alias { get; private set; }
    }
}
