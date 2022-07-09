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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Properties
        IDepartmentRepo _repo;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepo repo): base(repo)
        {
            _repo = repo;
        }
        #endregion

        #region Methods
        protected override bool Validate(Department dep)
        {
            bool valid = true;
            // check position id khác null
            if (string.IsNullOrEmpty(dep.DepartmentId.ToString()))
            {
                valid = false;
                ErrorMessages.Add("Mã phòng ban không được trống");
            }

            // check position id trùng
            if (_repo.CheckExist(dep.DepartmentId))
            {
                valid = false;
                ErrorMessages.Add("Mã phòng ban đã tồn tại");
            }

            // check position name khác null
            if (string.IsNullOrEmpty(dep.DepartmentName))
            {
                valid = false;
                ErrorMessages.Add("Tên phòng ban không được trống");
            }
            return valid;
        }

        #endregion
    }
}
