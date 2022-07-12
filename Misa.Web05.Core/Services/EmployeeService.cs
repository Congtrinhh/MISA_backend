using Microsoft.AspNetCore.Http;
using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;
using Misa.Web05.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Services
{
    /// <summary>
    /// Service cho đối tượng Employee
    /// Created by Trinh quy cong 5/7/22
    /// </summary>
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Properties
        /// <summary>
        /// repo để tương tác với DB: thêm, sửa, xoá, đọc
        /// </summary>
        private IEmployeeRepo _employeeRepo;
        #endregion

        #region Contructor
        public EmployeeService(IEmployeeRepo employeeRepo) : base(employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        /// <summary>
        /// thực hiện import employee từ file excel
        /// và trả về tất cả employee tham gia vào quá trình import này
        /// </summary>
        /// <param name="file">file excel</param>
        /// <returns>Tất cả employee tham gia vào quá trình import</returns>
        public IEnumerable<Employee> Import(IFormFile file)
        {
            // Validate tệp

            // Định dạng tệp

            // 

            return null;
        }

        /// <summary>
        /// Validate employee
        /// </summary>
        /// <param name="emp">employee</param>
        /// <returns>true nếu hợp lệ; false nếu không hợp lệ</returns>
        protected override bool Validate(Employee emp)
        {
            bool valid = true;
            // check employee id khác null
            if (string.IsNullOrEmpty(emp.EmployeeId.ToString()))
            {
                valid = false;
                ErrorMessages.Add("Mã nhân viên không được trống");
            }

            // check employee id trùng (chỉ check khi thực hiện thêm entity)
            if (base.CrudMode.Equals(Enums.CrudMode.Add))
            {
                if (_employeeRepo.CheckExist(emp.EmployeeId))
                {
                    valid = false;
                    ErrorMessages.Add("Mã nhân viên đã tồn tại");
                }
                if (_employeeRepo.CheckExist(emp.EmployeeCode))
                {
                    valid = false;
                    ErrorMessages.Add("Mã code nhân viên đã tồn tại");
                }
            }

            // kiểm tra xem employee code mới đã tồn tại trong DB hay chưa
            if (base.CrudMode.Equals(Enums.CrudMode.Update))
            {
                var exists = this._employeeRepo.CheckExist(emp.EmployeeCode);
                if (exists)
                {
                    valid = false;
                    ErrorMessages.Add("Mã code nhân viên đã tồn tại");
                }
            }

            // check employee code khác null
            if (string.IsNullOrEmpty(emp.EmployeeCode))
            {
                valid = false;
                ErrorMessages.Add("Mã code nhân viên không được trống");
            }

            // check employee code đúng định dạng
            var patternEmployeeCode = @"^NV-\d{4,8}$";
            Regex regexEmployeeCode = new Regex(patternEmployeeCode);
            if (!regexEmployeeCode.IsMatch(emp.EmployeeCode))
            {
                valid = false;
                ErrorMessages.Add("Mã code nhân viên không hợp lệ");
            }

            // check position name khác null
            if (string.IsNullOrEmpty(emp.FullName))
            {
                valid = false;
                ErrorMessages.Add("Tên nhân viên không được trống");
            }

            // validate email
            if (!string.IsNullOrEmpty(emp.Email) && !CommonMethods.IsEmailValid(emp.Email))
            {
                valid = false;
                ErrorMessages.Add("Email không hợp lệ");
            }

            // validate date of birth smaller than current date
            //...
            if (emp.DateOfBirth != null)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)emp.DateOfBirth) < 0)
                {
                    valid = false;
                    ErrorMessages.Add("Ngày sinh không thể lớn hơn hôm nay");
                }
            }

            // validate phone number

            if (!string.IsNullOrEmpty(emp.PhoneNumber) && !CommonMethods.IsPhoneNumberValid(emp.PhoneNumber))
            {
                valid = false;
                ErrorMessages.Add("Số điện thoại không hợp lệ");
            }

            return valid;
        }
        #endregion

        #region Methods

        #endregion
    }
}
