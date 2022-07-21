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

                // lặp qua và gán các id vào mệnh đề IN
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
        /// lấy ra ds tất cả employee
        /// </summary>
        /// <returns>list employee</returns>
        public override IEnumerable<Employee> GetAll()
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var sql = "SELECT * FROM View_Employee";
                
                // trả về danh sách employee
                return Conn.Query<Employee>(sql: sql).ToList();
            }
        }


        /// <summary>
        /// get employee by id, sử dụng procedure thay vì câu lệnh sql thường
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>employee với id tương ứng</returns>
        public override Employee GetById(Guid id)
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var sql = "Proc_GetEmployeeById";

                // thêm tham số
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", id);

                // trả về employee hoặc null nếu không tìm được
                var employee = Conn.QueryFirstOrDefault<Employee>(sql: sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
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