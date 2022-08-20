using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Interfaces.Repos
{
    /// <summary>
    /// Base interface cho các thao tác tương tác cơ bản với DB: thêm/sửa/xoá/get dữ liệu
    /// </summary>
    /// <typeparam name="MISAEntity">Tên 1 class như User, Department,..</typeparam>
    /// CreatedBy TQCONG 29/7/2022
    public interface IBaseRepo<MISAEntity> where MISAEntity: BaseEntity
    {
        /// <summary>
        /// Lấy ra danh sách entity dựa vào kết quả tìm kiếm và phân trang
        /// </summary>
        /// <param name="paginationRequest">Object chứa các trường để lọc dữ liệu</param>
        /// <returns>Đối tượng chứa danh sách entity và các thông tin phân trang</returns>
        /// CreatedBy TQCONG 2/8/2022 
        Task<PaginationResponse<MISAEntity>> GetPagingAsync(BasePaginationRequest paginationRequest);

        /// <summary>
        /// Lấy ra list entity
        /// </summary>
        /// <returns>List entity</returns>
        /// CreatedBy TQCONG 29/7/2022 
        Task<IEnumerable<MISAEntity>> GetAllAsync();

        /// <summary>
        /// Lấy ra entity theo id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity tương ứng</returns>
        /// CreatedBy TQCONG 29/7/2022 
        Task<MISAEntity> GetByIdAsync(int id);

        /// <summary>
        /// Thêm entity 
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>1 nếu thêm thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        Task<int> InsertAsync(MISAEntity entity);

        /// <summary>
        /// Cập nhật entity
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        /// <returns>1 nếu sửa thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        Task<int> UpdateAsync(MISAEntity entity);

        /// <summary>
        /// Xoá entity
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>1 nếu xoá thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        Task<int> DeleteAsync(int id);
    }
}
