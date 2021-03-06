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
    /// Created by Trinh Quy Cong 5/7/22
    /// </summary>
    public interface IEmployeeRepo:IBaseRepo<Employee>
    {
        /// <summary>
        /// Xoá nhiều employee
        /// </summary>
        /// <param name="listOfId">danh sách id</param>
        /// <returns>số bản ghi được xoá</returns>
        int DeleteMany(Guid[] listOfId);

        /// <summary>
        /// insert list employees from excel file import
        /// return the number of inserted employees
        /// </summary>
        /// <param name="employees">Danh sách employee</param>
        /// <returns>Số bản ghi được import thành công</returns>
        int Import(List<Employee> employees);

        /// <summary>
        /// get new employee code to insert new employee
        /// </summary>
        /// <returns>Employee code mới</returns>
        string GetNewEmployeeCode();

        /// <summary>
        /// return true if employee code
        /// return false otherwise
        /// </summary>
        /// <param name="id">Employee code</param>
        /// <returns>true - nếu đối tượng tồn tại; false - ngược lại</returns>
        bool CheckExist(string employeeCode);

        /// <summary>
        /// get employee by employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>employee</returns>
        Employee GetByEmployeeCode(string employeeCode);
    }
}
