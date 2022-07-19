using Misa.Web05.Core.Enums;
using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Services
{
    /// <summary>
    /// Lớp thực thi interface tổng quát của các service
    /// Created by Trinh Quy Cong 5/7/22
    /// </summary>
    /// <typeparam name="MISAEntity">Employee/Department/Positions/...</typeparam>
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {

        #region Properties
        /// <summary>
        /// Đối tượng tương tác với DB (thêm/sửa/xoá/đọc)
        /// </summary>
        private IBaseRepo<MISAEntity> _repo;

        /// <summary>
        /// Mảng thông báo lỗi
        /// </summary>
        protected List<string> ErrorMessages = new List<string>();

        /// <summary>
        /// Chế độ thao tác với DB (thêm/sửa/xoá)
        /// </summary>
        protected CrudMode CrudMode = CrudMode.Add;
        #endregion

        #region Contructor
        public BaseService(IBaseRepo<MISAEntity> repo)
        {
            _repo = repo;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate đối tượng, sau đó thêm vào DB
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// <exception cref="MISAValidationException">Thông báo lỗi cho người dùng</exception>
        public int Insert(MISAEntity entity)
        {
            // set chế độ thành thêm
            this.CrudMode = CrudMode.Add;

            // nếu dữ liệu đầu vào không hợp lệ, ném ra exception dừng quá trình insert
            if (!Validate(entity))
            {
                throw new MISAValidationException(ErrorMessages);
            };

            // dữ liệu hợp lệ, thực hiện insert và trả về kết quả insert thành công hay không
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Validate đối tượng, sau đó cập nhật 
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// <exception cref="MISAValidationException">Thông báo lỗi cho người dùng</exception>
        /// <exception cref="MISAValidationException"></exception>
        public int Update(MISAEntity entity)
        {
            // set chế độ thành cập nhật
            this.CrudMode = CrudMode.Update;

            // nếu dữ liệu đầu vào không hợp lệ, ném ra exception dừng quá trình update
            if (!Validate(entity))
            {
                throw new MISAValidationException(ErrorMessages);
            };

            // dữ liệu hợp lệ, thực hiện update và trả về kết quả update thành công hay không
            return _repo.Update(entity);
        }



        /// <summary>
        /// validate đối tượng nhận vào
        /// các lớp con sẽ override phương thức này nếu muốn validate theo cách của nó
        /// </summary>
        /// <param name="employee">đối tượng</param>
        /// <returns>true-nếu hợp lệ; false nếu không hợp lệ</returns>
        protected virtual bool Validate(MISAEntity entity)
        {
            // mặc định trả về true nếu các lớp con không override
            return true;
        }
        #endregion
    }
}
