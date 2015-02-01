using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public struct ColumnDefinition
    {
        public string Table { get; private set; }
        public string ColumnName { get; private set; }

        public ColumnDefinition(string table, string column) : this()
        {
            this.Table = table;
            this.ColumnName = column;
        }
    }
}
