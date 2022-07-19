using Misa.Web05.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using Misa.Web05.Core.Exceptions;

namespace Misa.Web05.Core.Services
{
    /// <summary>
    /// Service cho đối tượng Department
    /// Created by Trinh quy cong 5/7/22
    /// </summary>
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Properties
        /// <summary>
        /// repo để tương tác với DB: thêm, sửa, xoá, đọc
        /// </summary>
        IDepartmentRepo _repo;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepo repo): base(repo)
        {
            _repo = repo;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate phòng ban
        /// </summary>
        /// <param name="dep">đối tượng</param>
        /// <returns>true nếu hợp lệ; false nếu không hợp lệ</returns>
        protected override bool Validate(Department dep)
        {
            bool valid = true;
            // check department id khác null
            if (string.IsNullOrEmpty(dep.DepartmentId.ToString()))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.DepartmentIdNull);
            }

            // check department id trùng
            if (_repo.CheckExist(dep.DepartmentId))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.DepartmentIdExists);
            }

            // check department name khác null
            if (string.IsNullOrEmpty(dep.DepartmentName))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.DepartmentNameNull);
            }
            return valid;
        }

        #endregion
    }
}
