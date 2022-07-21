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

            // check employee code khác null
            if (string.IsNullOrEmpty(emp.EmployeeCode))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.EmployeeCodeNull);
            }

            // check employee id, employee code trùng (chỉ check khi thực hiện thêm entity)
            if (base.CrudMode.Equals(Enums.CrudMode.Add))
            {
                if (_employeeRepo.CheckExist(emp.EmployeeCode))
                {
                    valid = false;
                    ErrorMessages.Add(Resources.ExceptionErrorMessage.EmployeeCodeExists);
                }
            }

            // kiểm tra xem employee code mới đã tồn tại trong DB hay chưa (chỉ check khi thực hiện sửa entity)
            if (base.CrudMode.Equals(Enums.CrudMode.Update))
            {
                // check employee id khác null (khi thêm mới không cần employeeId vì trong DB tự sinh)
                if (string.IsNullOrEmpty(emp.EmployeeId.ToString()))
                {
                    valid = false;
                    ErrorMessages.Add(Resources.ExceptionErrorMessage.EmployeeIdNull);
                }
                // nhân viên cần update
                var currentEmployee = _employeeRepo.GetById(emp.EmployeeId);
                // nhân viên theo mã code mới
                var employeeFromDB = _employeeRepo.GetByEmployeeCode(emp.EmployeeCode);
                // nếu 2 nhân viên này khác nhau, tức mã code mới đã tồn tại
                if (!currentEmployee.EmployeeId.Equals(employeeFromDB.EmployeeId))
                {
                    valid = false;
                    ErrorMessages.Add(Resources.ExceptionErrorMessage.EmployeeCodeExists);
                }
            }

            // check employee code đúng định dạng; ví dụ định dạng hợp lệ: NV-12345789
            var patternEmployeeCode = @"^NV-\d{8}$";
            Regex regexEmployeeCode = new Regex(patternEmployeeCode);
            if (!regexEmployeeCode.IsMatch(emp.EmployeeCode))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.EmployeeCodeInvalid);
            }

            // check full name khác null
            if (string.IsNullOrEmpty(emp.FullName))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.FullNameNull);
            }

            // validate email
            if (!string.IsNullOrEmpty(emp.Email) && !CommonMethods.IsEmailValid(emp.Email))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.EmailInvalid);
            }

            // validate date of birth smaller than current date
            if (emp.DateOfBirth != null)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)emp.DateOfBirth) < 0)
                {
                    valid = false;
                    ErrorMessages.Add(Resources.ExceptionErrorMessage.DateOfBirthBiggerThanCurrentDate);
                }
            }

            // validate identity date smaller than current date
            if (emp.IdentityDate != null)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)emp.IdentityDate) < 0)
                {
                    valid = false;
                    ErrorMessages.Add(Resources.ExceptionErrorMessage.IdentityDateBiggerThanCurrentDate);
                }
            }

            // validate phone number
            if (!string.IsNullOrEmpty(emp.PhoneNumber) && !CommonMethods.IsPhoneNumberValid(emp.PhoneNumber))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.PhoneNumberInvalid);
            }

            return valid;
        }
        #endregion

        #region Methods

        #endregion
    }
}
