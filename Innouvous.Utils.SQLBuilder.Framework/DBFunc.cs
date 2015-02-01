using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework
{
    /// <summary>
    /// Simple class to wrap function calls so they are not treated as strings (quoted)
    /// </summary>
    public class DBFunc
    {
        public string Function { get; private set; }

        public DBFunc(string function)
        {
            this.Function = function;
        }

        public override string ToString()
        {
            return Function;
        }
    }
}
