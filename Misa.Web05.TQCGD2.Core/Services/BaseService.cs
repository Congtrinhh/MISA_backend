using Misa.Web05.TQCGD2.Core.Enums;
using Misa.Web05.TQCGD2.Core.Exceptions;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Services
{
    /// <summary>
    /// Class implement interface IBaseService
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    /// CreatedBy TQCONG 4/8/2022
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : BaseEntity
    {
        #region Property
        protected IBaseRepo<MISAEntity> Repo;

        /// <summary>
        /// Mảng thông báo lỗi
        /// </summary>
        protected List<string> ErrorMessages = new List<string>();

        /// <summary>
        /// Chế độ thao tác với DB - toàn cục
        /// </summary>
        protected CrudMode CrudMode = CrudMode.Add;
        #endregion

        #region Contructor
        public BaseService(IBaseRepo<MISAEntity> repo)
        {
            Repo = repo;
        }
        #endregion

        #region Method

        /// <summary>
        /// Tạo mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task<int> InsertAsync(MISAEntity entity)
        {
            CrudMode = CrudMode.Add;

            if (Validate(entity) == false)
            {
                throw new MISAValidationException(ErrorMessages);
            }

            await BeforeInsert(entity);

            int rs = await DoInsert(entity);

            await AfterInsert(entity);

            return rs;
        }

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task<int> UpdateAsync(MISAEntity entity)
        {
            CrudMode = CrudMode.Update;

            if (Validate(entity) == false)
            {
                throw new MISAValidationException(ErrorMessages);
            }

            int rs = await DoUpdate(entity);
            return rs;
        }

        /// <summary>
        /// validate các trường dữ liệu của entity
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>true nếu hợp lệ; ngược lại false</returns>
        /// CreatedBy TQCONG 4/8/2022
        public virtual bool Validate(MISAEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Thực hiện những việc cần làm trước khi thêm mới
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task BeforeInsert(MISAEntity entity) { }

        /// <summary>
        /// Thực hiện thêm mới
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task<int> DoInsert(MISAEntity entity)
        {
            return await Repo.InsertAsync(entity);
        }

        /// <summary>
        /// Thực hiện những việc cần làm sau khi thêm mới
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task AfterInsert(MISAEntity entity) { }

        /// <summary>
        /// Thực hiện những việc cần trước sau khi cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual void BeforeUpdate(MISAEntity entity) { }

        /// <summary>
        /// Thực hiện cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual async Task<int> DoUpdate(MISAEntity entity) { return await Task.FromResult(1); }

        /// <summary>
        /// Thực hiện những việc cần làm sau khi cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// CreatedBy TQCONG 4/8/2022
        public virtual void AfterUpdate(MISAEntity entity) { }
        #endregion
    }
}
