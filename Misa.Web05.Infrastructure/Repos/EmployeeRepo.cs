using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Misa.Web05.Infrastructure.Repos
{
    /// <summary>
    /// Repo cho employee
    /// Created by Trinh Quy Cong 9/7/2022
    /// </summary>
    public class EmployeeRepo : BaseRepo<Employee>, IEmployeeRepo
    {
        /// <summary>
        /// check employee code tồn tại
        /// </summary>
        /// <param name="employeeCode">mã code</param>
        /// <returns>true nếu tồn tại;ngược lại false</returns>
        public bool CheckExist(string employeeCode)
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                var sql = "SELECT * FROM Employee WHERE EmployeeCode=@employeeCode";
                var parameters = new DynamicParameters();
                parameters.Add("@employeeCode", employeeCode);
                var employee = Conn.Query<Employee>(sql, parameters);
                if (employee != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// lấy ra employee code mới
        /// </summary>
        /// <returns>new employee code</returns>
        public string getNewEmployeeCode()
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                var res = Conn.QueryFirstOrDefault<string>(
                    "Proc_GetNewEmployeeCode", commandType: System.Data.CommandType.StoredProcedure
                    );
                return res;
            }
        }

        /// <summary>
        /// thêm dữ liệu vào DB và trả về số bản ghi được thêm thành công
        /// </summary>
        /// <param name="employees">danh sách nhân viên</param>
        /// <returns>số bản ghi được thêm thành công</returns>
        public int Import(List<Employee> employees)
        {
            return 0;
        }
    }
}