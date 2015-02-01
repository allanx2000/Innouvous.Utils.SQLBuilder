using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public struct ComplexColumn : IColumn
    {
        public string Formula { get; private set; }
        public string Alias { get; private set; }

        public ComplexColumn(string formula, string alias = null) : this()
        {
            this.Formula = formula;
            this.Alias = alias;
        }
    }
}
