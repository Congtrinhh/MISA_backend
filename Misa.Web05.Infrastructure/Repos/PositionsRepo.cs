using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Infrastructure.Repos
{
    /// <summary>
    /// Repo cho đối tượng Positions để tương tác với database
    /// Created by TQCONG 5/7/22
    /// </summary>
    public class PositionsRepo : BaseRepo<Positions>, IPositionsRepo
    {
        #region Constructor
        public PositionsRepo()
        {
            // lấy ra từ 'Positions'
            base.SqlTableName = typeof(Positions).Name.ToString();
            // lấy ra từ 'Position'; cần làm việc này vì trong table Positions có trường PositionId (không có s)
            // , ta sẽ dùng property SqlEntityName kết hợp với SqlTableName để viết câu lệnh sql
            base.SqlEntityName = base.SqlTableName.Substring(0, base.SqlTableName.Length - 1);
        }
        #endregion
    }
}
