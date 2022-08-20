using Misa.Web05.TQCGD2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Interfaces.Services
{
    /// <summary>
    /// Service interface cho class User
    /// </summary>
    /// CreatedBy TQCONG 4/8/2022
    public interface IUserService: IBaseService<User>
    {
        /// <summary>
        /// Tạo nhiều user
        /// </summary>
        /// <param name="users">Danh sách user</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        /// CreatedBy TQCONG 8/8/2022
        Task<int> InsertManyAsync(IEnumerable<User> users);
    }
}
