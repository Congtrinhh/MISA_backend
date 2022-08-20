using Dapper;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Models.Paging;
using Misa.Web05.TQCGD2.Core.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Infrastructure.Repos
{
    /// <summary>
    /// Class thực thi inteface IUserRepo
    /// </summary>
    /// CreatedBy TQCONG 29/7/2022
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        #region Method
        /// <summary>
        /// Lấy ra user theo id, kèm theo danh sách vai trò
        /// </summary>
        /// <param name="id">Id của user</param>
        /// <returns>User</returns>
        public override async Task<User> GetByIdAsync(int id)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                SetSqlStatementGetById();

                var sqlGetUser = $"SELECT {SqlStatementGetById.Select} FROM {SqlStatementGetById.From} WHERE {SqlStatementGetById.Where}";
                var sqlGetRoles = $"SELECT r.* FROM user u INNER JOIN user_role ur ON u.UserID = ur.UserID INNER JOIN role r " +
                    $" ON ur.RoleID = r.RoleID WHERE u.UserID=@userId";

                var sql = sqlGetUser + ";" + sqlGetRoles;
                List<List<object>> listQueryResult = new List<List<object>>();

                List<Type> types = new List<Type>();
                types.Add(typeof(User));
                types.Add(typeof(Role));

                // execute ra các kết quả DataReader
                using (var multi = await Conn.QueryMultipleAsync(sql, new { userId=id}))
                {
                    int index = 0;
                    do
                    {
                        // đọc lần lượt từng kết quả DataReader, thêm vào list result
                        listQueryResult.Add(multi.Read(types[index]).ToList());
                        index++;
                    } while (!multi.IsConsumed);
                }

                // ép kiểu về User bằng cách Serialize sau đó Deserialize
                var user = JsonSerializer.Deserialize<User>(
                    JsonSerializer.Serialize(listQueryResult[0].FirstOrDefault())
                    );

                if (user is not null)
                {
                    var tempListRole = listQueryResult[1];
                    var listRole = new List<Role>();

                    // ép từng phần tử trong mảng role về kiểu Role
                    foreach (var role in tempListRole)
                    {
                        if (role is not null)
                        {
                            listRole.Add(
                                JsonSerializer.Deserialize<Role>(
                    JsonSerializer.Serialize(role)
                    )
                                );
                        }
                    }

                    // gán ds role của user bằng role kết quả
                    user.Roles = listRole;
                    return user;
                } else
                {
                    throw new Exception(ExceptionErrorMessage.UserNotFound);
                }
            }
        }


        ///// <summary>
        ///// Lấy danh sách user (không cần lấy vai trò vì đã có property lưu các tên vai trò)
        ///// </summary>
        ///// <returns>List user</returns>
        //public override async Task<IEnumerable<User>> GetAll()
        //{
        //    return await base.GetAll();
        //}

        /// <summary>
        /// Lấy ra user theo mã UserCode
        /// </summary>
        /// <param name="userCode">mã user code</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        public async Task<User> GetByUserCodeAsync(string userCode)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM User WHERE UserCode=@code";
                User rs = await Conn.QueryFirstOrDefaultAsync<User>(sql, new { code = userCode });
                return rs;
            }
        }

        /// <summary>
        /// Overrride: custom lại param để có thể truyền vào dapper, 
        /// vì dapper không nhận dữ liệu kiểu như Department hoặc Position
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        public override async Task<int> InsertAsync(User entity)
        {
            
            using (Conn = new MySqlConnection(ConnectionString))
            {

                var objectParam = new
                {
                    UserCode = entity.UserCode,
                    FullName = entity.FullName,
                    Email = entity.Email,
                    Status = entity.Status,
                    DepartmentId = entity.Department!=null ? entity.Department.DepartmentId : -1,
                    PositionId = entity.Position != null ? entity.Position.PositionId : -1,
                    RoleNames = entity.RoleNames,
                };

                string sql = $"Proc_Insert{TableName}";
                return await Conn.ExecuteAsync(sql: sql, param: objectParam, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Overrride: custom lại param để có thể truyền vào dapper, 
        /// vì dapper không nhận dữ liệu kiểu như Department hoặc Position
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        public override async Task<int> UpdateAsync(User entity)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                var objectParam = new
                {
                    UserId = entity.UserId,
                    UserCode = entity.UserCode,
                    FullName = entity.FullName,
                    Email = entity.Email,
                    Status = entity.Status,
                    DepartmentId = entity.Department?.DepartmentId,
                    PositionId = entity.Position?.PositionId,
                    RoleNames = entity.RoleNames,
                };

                string sql = $"Proc_Update{TableName}";
                return await Conn.ExecuteAsync(sql: sql, param: objectParam, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Set up câu lệnh sql cho method GetPaging
        /// </summary>
        /// <param name="paginationRequest">Dùng để build mệnh đề WHERE</param>
        /// CreatedBy TQCONG 8/8/2022
        public override void SetSqlStatementGetPaging(BasePaginationRequest paginationRequest)
        {
            // Build mệnh đề SELECT
            this.SqlStatementGetPaging.Select = "u.*, d.Name AS DepartmentName, p.Name AS PositionName";
            // Build mệnh đề FROM
            this.SqlStatementGetPaging.From = "user u INNER JOIN user_role ur ON u.UserID = ur.UserID INNER JOIN role r ON ur.RoleID = r.RoleID " +
                " LEFT OUTER JOIN department d ON d.DepartmentID=u.DepartmentID LEFT OUTER JOIN positions p ON u.PositionID = p.PositionID";
            // Build mệnh đề ORDER BY
            this.SqlStatementGetPaging.OrderBy = "u.CreatedDate DESC";

            // Build mệnh đề WHERE
            StringBuilder sb = new StringBuilder();
            // cast class và nhận về class mới với kiểu đã được cast
            if (paginationRequest is UserPaginationRequest userPaginationRequest)
            {
                if (!string.IsNullOrEmpty(userPaginationRequest.Keyword))
                {
                    sb.Append(
                        $" AND (u.FullName LIKE '%{userPaginationRequest.Keyword}%' OR u.Email LIKE '%{userPaginationRequest.Keyword}%' " + 
                        $" OR u.UserCode LIKE '%{userPaginationRequest.Keyword}%' OR d.Name LIKE '%{userPaginationRequest.Keyword}%' " +
                        $" OR p.Name LIKE '%{userPaginationRequest.Keyword}%')"
                        );
                }
                if (userPaginationRequest.RoleId != null && userPaginationRequest.RoleId > 0)
                {
                    sb.Append($" AND r.RoleID={userPaginationRequest.RoleId}");
                }
            }
            sb.Append(" GROUP BY u.UserID"); // thêm mệnh đề GROUP BY vì khi join với bảng role, có khả năng sẽ sinh ra nhiều bản ghi với mỗi user
            this.SqlStatementGetPaging.Where = sb.ToString();
        }

        /// <summary>
        /// Set up câu lệnh sql cho method GetById
        /// </summary>
        /// CreatedBy TQCONG 8/8/2022
        public override void SetSqlStatementGetById()
        {
            this.SqlStatementGetById.Select = "u.*, d.Name AS DepartmentName, p.Name AS PositionName";
            this.SqlStatementGetById.From = "user u LEFT OUTER JOIN department d ON u.DepartmentID = d.DepartmentID " + 
                " LEFT OUTER JOIN positions p ON u.PositionID = p.PositionID";
            this.SqlStatementGetById.Where = $"u.UserID=@{EntityName}ID";
        }

        /// <summary>
        /// Set up câu lệnh sql cho method GetAll
        /// </summary>
        /// CreatedBy TQCONG 8/8/2022
        public override void SetSqlStatementGetAll()
        {
            this.SqlStatementGetAll.Select = "u.*, d.Name AS DepartmentName, p.Name AS PositionName";
            this.SqlStatementGetAll.From = "user u LEFT OUTER JOIN department d ON u.DepartmentID = d.DepartmentID " + 
                "LEFT OUTER JOIN positions p ON u.PositionID = p.PositionID";
            this.SqlStatementGetAll.OrderBy = "u.CreatedDate DESC";
        }

        /// <summary>
        /// lấy về mã userCode mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy TQCONG 14/8/2022
        public async Task<string> GetNewUserCodeAsync()
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = "Proc_GetNewUserCode";
                return await Conn.QueryFirstOrDefaultAsync<string>(sql, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}
