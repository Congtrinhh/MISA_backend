using Misa.Web05.TQCGD2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Interfaces.Repos
{
    /// <summary>
    /// Interface repo cho class User
    /// </summary>
    /// CreatedBy TQCONG 29/7/2022
    public interface IUserRepo:IBaseRepo<User>
    {
        /// <summary>
        /// Lấy ra user theo mã UserCode
        /// </summary>
        /// <param name="userCode">mã user code</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        Task<User> GetByUserCodeAsync(string userCode);

        /// <summary>
        /// lấy về mã userCode mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy TQCONG 14/8/2022
        Task<string> GetNewUserCodeAsync();
    }
}
