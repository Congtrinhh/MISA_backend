using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Repos
{
    /// <summary>
    /// Interface repo cho đối tượng Employee
    /// Created by TQCONG 5/7/2022
    /// </summary>
    public interface IEmployeeRepo:IBaseRepo<Employee>
    {
        #region Methods

        /// <summary>
        /// Xoá nhiều employee
        /// </summary>
        /// <param name="listOfId">Danh sách id</param>
        /// <returns>Số bản ghi được xoá</returns>
        /// Created by TQCONG 5/7/2022
        int DeleteMany(Guid[] listOfId);

        /// <summary>
        /// Thêm một danh sách nhân viên vào database
        /// từ file excel nhập khẩu
        /// </summary>
        /// <param name="employees">Danh sách employee</param>
        /// <returns>Số bản ghi được import thành công</returns>
        /// Created by TQCONG 5/7/2022
        int Import(List<Employee> employees);

        /// <summary>
        /// Lấy mã employee mới (employeeCode)
        /// </summary>
        /// <returns>Employee code mới</returns>
        /// Created by TQCONG 5/7/2022
        string GetNewEmployeeCode();

        /// <summary>
        /// Trả về true nếu đôi tượng tồn tại; ngược lại, trả về false
        /// </summary>
        /// <param name="id">Employee code</param>
        /// <returns>true - nếu đối tượng tồn tại; false - ngược lại</returns>
        /// Created by TQCONG 5/7/2022
        bool CheckExist(string employeeCode);

        /// <summary>
        /// Lấy ra employee theo employeeCode
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>Employee tương ứng</returns>
        /// Created by TQCONG 5/7/2022
        Employee GetByEmployeeCode(string employeeCode);
        #endregion
    }
}
