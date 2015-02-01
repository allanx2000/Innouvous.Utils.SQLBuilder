using Innouvous.Utils.SQLBuilder.Framework.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework
{
    public interface ISQLQueryWriter
    {
        string CreateSelectQuery(SelectModel model);

        string CreateInsertQuery(InsertModel query);

        string CreateUpdateQuery(UpdateModel query);

        string CreateDeleteQuery(DeleteModel query);
    }
}
