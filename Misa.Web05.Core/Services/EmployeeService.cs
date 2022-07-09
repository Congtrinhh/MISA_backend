using Microsoft.AspNetCore.Http;
using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Services
{
    // abstract base service
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Properties
        private IEmployeeRepo _employeeRepo;
        #endregion

        #region Contructor
        public EmployeeService(IEmployeeRepo employeeRepo):base(employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public IEnumerable<Employee> Import(IFormFile file)
        {
            // Validate tệp

            // Định dạng tệp

            // 

            return null;
        }

        protected override bool Validate(Employee emp)
        {
            bool valid = true;
            // check employee id khác null
            if (string.IsNullOrEmpty(emp.EmployeeId.ToString()))
            {
                valid = false;
                ErrorMessages.Add("Mã nhân viên không được trống");
            }

            // check employee id trùng
            if (_employeeRepo.CheckExist(emp.EmployeeId))
            {
                valid = false;
                ErrorMessages.Add("Mã nhân viên đã tồn tại");
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
            var patternEmail = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            Regex regexEmail = new Regex(patternEmail);
            if (!regexEmail.IsMatch(emp.Email)) 
            {
                valid = false;
                ErrorMessages.Add("Email không hợp lệ");
            }

            // date of birth format and smaller than current date

            // validate phone number
            var patternPhone = @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
            Regex regexPhone = new Regex(patternPhone);
            if (!regexPhone.IsMatch(emp.PhoneNumber))
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
