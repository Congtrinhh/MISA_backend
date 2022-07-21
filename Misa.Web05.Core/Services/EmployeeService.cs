using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;
using Misa.Web05.Core.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
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
        /// xuất tất cả employee ra 1 file excel
        /// </summary>
        /// <returns>đối tượng Stream chứa excel file và các thông tin khác</returns>
        public Stream Export()
        {
            List<Employee> allEmployees = _employeeRepo.GetAll().ToList();

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                // tạo ra 1 sheet excel
                var worksheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

                // cột đầu tiên của table tính từ header (sẽ tạo table sau khi thêm hết các dòng vào worksheet)
                var rowStart = 3;
                // cột cuối cùng của table
                var rowEnd = allEmployees.Count + rowStart;


                // set dòng văn bản đầu tiên cho file excel
                worksheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                worksheet.Cells[1, 1, 1, 9].Merge = true;
                worksheet.Cells[2, 1].Value = "";
                worksheet.Cells[2, 1, 2, 9].Merge = true;


                // set header cho bảng của file excel
                // biến lưu giá trị cột số thứ tự
                var tableIndex = 1;
                worksheet.Cells[rowStart, 1].Value = "STT";
                worksheet.Cells[rowStart, 2].Value = "Mã nhân viên";
                worksheet.Cells[rowStart, 3].Value = "Tên nhân viên";
                worksheet.Cells[rowStart, 4].Value = "Giới tính";
                worksheet.Cells[rowStart, 5].Value = "Ngày sinh";
                worksheet.Cells[rowStart, 6].Value = "Chức danh";
                worksheet.Cells[rowStart, 7].Value = "Tên đơn vị";
                worksheet.Cells[rowStart, 8].Value = "Số tài khoản";
                worksheet.Cells[rowStart, 9].Value = "Tên ngân hàng";



                // set nội dung (thông tin tất cả nhân viên) cho bảng của file excel (+1 vào rowStart để bỏ qua header)
                var forLoopIndex = rowStart + 1;
                foreach (var emp in allEmployees)
                {
                    worksheet.Cells[forLoopIndex, 1].Value = tableIndex;
                    worksheet.Cells[forLoopIndex, 2].Value = CommonMethods.GetEmptyStringIfNull(emp.EmployeeCode);
                    worksheet.Cells[forLoopIndex, 3].Value = CommonMethods.GetEmptyStringIfNull(emp.FullName);
                    worksheet.Cells[forLoopIndex, 4].Value = CommonMethods.GetEmptyStringIfNull(emp.GenderName);
                    worksheet.Cells[forLoopIndex, 5].Value = emp.DateOfBirth == null ? "" : emp.DateOfBirth;
                    worksheet.Cells[forLoopIndex, 6].Value = CommonMethods.GetEmptyStringIfNull(emp.PositionName);
                    worksheet.Cells[forLoopIndex, 7].Value = CommonMethods.GetEmptyStringIfNull(emp.DepartmentName);
                    worksheet.Cells[forLoopIndex, 8].Value = CommonMethods.GetEmptyStringIfNull(emp.BankAccountNumber);
                    worksheet.Cells[forLoopIndex, 9].Value = CommonMethods.GetEmptyStringIfNull(emp.BankName);

                    forLoopIndex++;
                    tableIndex++;
                }

                // tạo table và các cài đặt
                var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: rowStart, fromCol: 1, toRow: rowEnd, toColumn: 9), "dsnv_unique");

                // add border
                worksheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                // hiện header
                tbl.ShowHeader = true;
                // add style cho toàn bộ table
                tbl.TableStyle = TableStyles.Medium2;

                // AutoFit Columns
                worksheet.Cells[rowStart, 1, rowEnd, 9].AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
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
