using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Repos
{
    /// <summary>
    /// Base interface for database manipulation
    /// CreatedBy Trinh Quy Cong - 5/7/2022
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public interface IBaseRepo<MISAEntity>
    {
        /// <summary>
        /// lấy ra các entity và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex">trang cần lấy (tính từ 0)</param>
        /// <param name="size">số bản ghi của trang</param>
        /// <param name="keyword">từ khoá tìm kiếm</param>
        /// <returns>Paging object chứa thông tin phân trang</returns>
        Paging GetPaging(int pageIndex, int size, string keyword);
        /// <summary>
        /// get all entities
        /// </summary>
        /// <returns>Danh sách tất cả entity</returns>
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// get entity by its id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity với id tương ứng</returns>
        MISAEntity GetById(Guid id);

        /// <summary>
        /// return true if entity exists
        /// return false otherwise
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>true - nếu đối tượng tồn tại trong DB; false nếu ngược lại</returns>
        bool CheckExist(Guid id);

        /// <summary>
        /// add new entity into database
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>trả về 1 nếu thêm thành công</returns>
        int Insert(MISAEntity entity);

        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="entity">Entity cần sửa</param>
        /// <returns>trả về 1 nếu sửa thành công</returns>
        int Update(MISAEntity entity);

        /// <summary>
        /// delete entity from database
        /// </summary>
        /// <param name="id">Id của entity cần xoá</param>
        /// <returns>trả về 1 nếu xoá thành công</returns>
        int Delete(Guid id);
    }
}
