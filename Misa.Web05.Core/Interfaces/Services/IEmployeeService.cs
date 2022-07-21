using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Services
{
    /// <summary>
    /// Interface tổng quát của đối tượng Employee
    /// Created by Trinh Quy Cong 5/7/22
    /// </summary>
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// nhận vào file exel
        /// trả về danh sách employee tham gia quá trình import 
        /// (nếu import không thành công thì đối tượng sẽ chứa thông báo lỗi)
        /// </summary>
        /// <param name="file">file excel</param>
        /// <returns>Các employee được đưa vào quá trình import</returns>
        IEnumerable<Employee> Import(IFormFile file);

        /// <summary>
        /// xuất tất cả employee ra 1 file excel
        /// </summary>
        /// <returns>đối tượng Stream chứa excel file và các thông tin khác</returns>
        Stream Export();
    }
}
