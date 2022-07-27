using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Services
{
    /// <summary>
    /// Interface tổng quát cho các service 
    /// Created by TQCONG 5/7/2022
    /// </summary>
    /// <typeparam name="MISAEntity">Một trong các class như Employee, Department,..</typeparam>
    public interface IBaseService<MISAEntity>
    {
        #region Methods

        /// <summary>
        /// Thực hiện logic nghiệp vụ và thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 - nếu thêm thành công</returns>
        /// CreatedBy TQCONG 5/7/2022
        int Insert(MISAEntity entity);

        /// <summary>
        /// Thực hiện logic nghiệp vụ và thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 - nếu thêm thành công</returns>
        /// CreatedBy TQCONG 5/7/2022
        int Update(MISAEntity entity);
        #endregion
    }
}
