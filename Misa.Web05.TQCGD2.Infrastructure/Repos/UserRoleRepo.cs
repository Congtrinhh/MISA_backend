using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Misa.Web05.TQCGD2.Infrastructure.Repos
{
    public class UserRoleRepo : BaseRepo<UserRole>, IUserRoleRepo
    {
        #region Contructor

        public UserRoleRepo()
        {
            this.TableName = "User_role";
            this.EntityName = "User_role";
        }

        #endregion

        #region Method
        /// <summary>
        /// Xoá user_role dự vào khoá chính
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 5/8/2022
        public async Task<int> DeleteAsync(int userId, int roleId)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM {TableName} WHERE UserID=@userId AND RoleID=@roleId";
                int rs = await Conn.ExecuteAsync(sql, param: new { userId, roleId });
                return rs;
            }
        }
        #endregion

    }
}
