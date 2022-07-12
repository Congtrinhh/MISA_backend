using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Services
{
    /// <summary>
    /// Interface tổng quát cho các service 
    /// Created by Trinh Quy Cong 5/7/22
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Thực hiện logic nghiệp vụ và thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 - nếu thêm thành công</returns>
        int Insert(MISAEntity entity);

        /// <summary>
        /// Thực hiện logic nghiệp vụ và thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 - nếu thêm thành công</returns>
        int Update(MISAEntity entity);
    }
}
