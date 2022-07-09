using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Infrastructure.Repos
{
    public class PositionsRepo : BaseRepo<Positions>, IPositionsRepo
    {
        public PositionsRepo()
        {
            base.SqlTableName = typeof(Positions).Name.ToString();
            base.SqlEntityName = base.SqlTableName.Substring(0, base.SqlTableName.Length - 1);
        }
    }
}
