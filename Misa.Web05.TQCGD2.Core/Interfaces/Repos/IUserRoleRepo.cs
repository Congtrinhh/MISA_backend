using Misa.Web05.TQCGD2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Interfaces.Repos
{
    /// <summary>
    /// Interface repo cho class UserRole
    /// </summary>
    /// CreatedBy TQCONG 4/8/2022
    public interface IUserRoleRepo:IBaseRepo<UserRole>
    {
        /// <summary>
        /// Xoá user_role
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <param name="roleId">Id của role</param>
        /// <returns>1 nếu thành công</returns>
        Task<int> DeleteAsync(int userId, int roleId);
    }
}
