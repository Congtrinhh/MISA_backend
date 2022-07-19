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
                // khởi tạo câu lệnh sql
                var sql = "SELECT * FROM Employee WHERE EmployeeCode=@employeeCode";

                // thêm tham số
                var parameters = new DynamicParameters();
                parameters.Add("@employeeCode", employeeCode);

                // lấy ra employee hoặc null nếu không tìm thấy
                var employee = Conn.QueryFirstOrDefault<Employee>(sql, parameters);

                // trả về kết quả
                if (employee != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Xoá nhiều employee
        /// </summary>
        /// <param name="listOfId">mảng employee id</param>
        /// <returns></returns>
        public int DeleteMany(Guid[] listOfId)
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var sql = $"DELETE FROM {SqlTableName} WHERE EmployeeId IN (";
                foreach(var id in listOfId)
                {
                    sql += $"'{id}',";
                }

                // bỏ dấu "," cuối
                sql = sql.Substring(0, sql.Length - 1);
                // thêm dấu đóng ngoặc
                sql += ")";

                var res = Conn.Execute(sql: sql);
                return res;
            }
        }

        /// <summary>
        /// get employee by employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>employee</returns>
        public Employee GetByEmployeeCode(string employeeCode)
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var sql = "SELECT * FROM Employee WHERE EmployeeCode=@employeeCode";

                // thêm tham số
                var parameters = new DynamicParameters();
                parameters.Add("@employeeCode", employeeCode);

                // trả về employee hoặc null nếu không tìm được
                var employee = Conn.QueryFirstOrDefault<Employee>(sql, parameters);
                return employee;
            }
        }

        /// <summary>
        /// lấy ra employee code mới
        /// </summary>
        /// <returns>new employee code</returns>
        public string GetNewEmployeeCode()
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var res = Conn.QueryFirstOrDefault<string>(
                    "Proc_GetNewEmployeeCode", commandType: System.Data.CommandType.StoredProcedure
                    );
                // trả về kết quả là mã employee code mới
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
            // tạm thời chưa thực thi
            return 0;
        }
    }
}