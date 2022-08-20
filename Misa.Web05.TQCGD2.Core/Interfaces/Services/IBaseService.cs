using Misa.Web05.TQCGD2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Interfaces.Services
{
    /// <summary>
    /// Service tổng quát 
    /// </summary>
    /// CreatedBy TQCONG 4/8/2022
    public interface IBaseService<MISAEntity> where MISAEntity : BaseEntity
    {
        /// <summary>
        /// Tạo đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 4/8/2022
        Task<int> InsertAsync(MISAEntity entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 4/8/2022
        Task<int> UpdateAsync(MISAEntity entity);
    }
}
