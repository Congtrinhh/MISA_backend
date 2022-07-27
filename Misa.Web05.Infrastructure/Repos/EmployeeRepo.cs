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
    /// Repo cho đối tượng Employee để tương tác với database
    /// Created by TQCONG 9/7/2022
    /// </summary>
    public class EmployeeRepo : BaseRepo<Employee>, IEmployeeRepo
    {
        #region Methods

        /// <summary>
        /// Check employee code tồn tại
        /// </summary>
        /// <param name="employeeCode">Mã code</param>
        /// <returns>true nếu tồn tại;ngược lại false</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// <param name="listOfId">Mảng employee id</param>
        /// <returns>Số bản ghi xoá thành công</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// Lấy ra employee theo employeeCode
        /// </summary>
        /// <param name="employeeCode">Mã employeeCode</param>
        /// <returns>Đố tượng employee</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// Lấy ra danh sách tất cả employee
        /// </summary>
        /// <returns>List employee</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// Lấy ra employee theo id, sử dụng procedure thay vì câu lệnh sql thường
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee với id tương ứng</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// Lấy ra employee code mới
        /// </summary>
        /// <returns>New employee code</returns>
        /// CreatedBy TQCONG 9/7/2022
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
        /// Thêm dữ liệu vào DB và trả về số bản ghi được thêm thành công
        /// </summary>
        /// <param name="employees">Danh sách nhân viên</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        /// CreatedBy TQCONG 9/7/2022
        public int Import(List<Employee> employees)
        {
            // TODO Import 
            return 0;
        }
        #endregion
    }
}