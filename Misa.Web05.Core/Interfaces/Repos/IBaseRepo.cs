using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Repos
{
    /// <summary>
    /// Base interface để tương tác với database
    /// CreatedBy TQCONG - 5/7/2022
    /// </summary>
    /// <typeparam name="MISAEntity">Một trong các class như Employee, Department,..</typeparam>
    public interface IBaseRepo<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy ra các entity và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex">Trang cần lấy (tính từ 0)</param>
        /// <param name="size">Số bản ghi của trang</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <returns>Paging object chứa thông tin phân trang</returns>
        /// CreatedBy TQCONG 5/7/2022
        Paging GetPaging(int pageIndex, int size, string keyword);

        /// <summary>
        /// Lấy ra tất cả entity
        /// </summary>
        /// <returns>Danh sách tất cả entity</returns>
        /// CreatedBy TQCONG 5/7/2022
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lẩy ra entity theo id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity với id tương ứng</returns>
        /// CreatedBy TQCONG 5/7/2022
        MISAEntity GetById(Guid id);

        /// <summary>
        /// Trả về true nếu tồn tại; ngược lại, trả về false
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>true - nếu đối tượng tồn tại trong DB; false nếu ngược lại</returns>
        /// CreatedBy TQCONG 5/7/2022
        bool CheckExist(Guid id);

        /// <summary>
        /// Tạo entity
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy TQCONG 5/7/2022
        int Insert(MISAEntity entity);

        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="entity">Entity cần sửa</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy TQCONG 5/7/2022
        int Update(MISAEntity entity);

        /// <summary>
        /// Xoá entity
        /// </summary>
        /// <param name="id">Id của entity cần xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy TQCONG 5/7/2022
        int Delete(Guid id);
        #endregion
    }
}
