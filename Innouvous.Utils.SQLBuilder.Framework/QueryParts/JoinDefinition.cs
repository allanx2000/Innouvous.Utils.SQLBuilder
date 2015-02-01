using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public class JoinDefinition
    {
        public enum JoinType
        {
            INNER,
            OUTER
        }

        public JoinType Type {get; private set;}
        public string JoinTable { get; private set; }
        
        private List<FieldColumn> columns;
        public List<FieldColumn> Columns
        {
            get
            {
                return new List<FieldColumn>(columns);
            }
        }

        public JoinDefinition(JoinType type, string jointable, params FieldColumn[] columnPairs)
        {
            Type = type;

            JoinTable = jointable;

            if (columnPairs.Length == 0)
                throw new Exception("Must define at 1 column pair");
            else if (columnPairs.Length % 2 == 1)
                throw new Exception("Columns must be in pairs");

            columns = new List<FieldColumn>(columnPairs);
        }

    }
}
