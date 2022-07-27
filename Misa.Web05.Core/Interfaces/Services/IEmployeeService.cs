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
    /// Created by TQCONG 5/7/2022
    /// </summary>
    public interface IEmployeeService: IBaseService<Employee>
    {
        #region Methods

        /// <summary>
        /// Nhận vào file exel
        /// Trả về danh sách employee tham gia quá trình import 
        /// (nếu import không thành công thì đối tượng sẽ chứa thông báo lỗi)
        /// </summary>
        /// <param name="file">File excel</param>
        /// <returns>Các employee được đưa vào quá trình import</returns>
        /// CreatedBy TQCONG 5/7/2022
        IEnumerable<Employee> Import(IFormFile file);

        /// <summary>
        /// Xuất tất cả employee ra 1 file excel
        /// </summary>
        /// <returns>Đối tượng Stream chứa excel file và các thông tin khác</returns>
        /// CreatedBy TQCONG 5/7/2022
        Stream Export();
        #endregion
    }
}
