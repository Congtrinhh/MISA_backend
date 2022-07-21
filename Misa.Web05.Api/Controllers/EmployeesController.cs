using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;
using Misa.Web05.Core.Resources;
using Misa.Web05.Core.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace Misa.Web05.Api.Controllers
{
    /// <summary>
    /// Created by trinh quy cong 5/7/22
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        #region Properties
        private IEmployeeService _employeeService;
        private IEmployeeRepo _employeeRepo;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepo employeeRepo)
        {
            _employeeService = employeeService;
            _employeeRepo = employeeRepo;
        }
        #endregion

        #region Methods

        /// <summary>
        /// xuất khẩu danh sách tất cả nhân viên có trong DB ra 1 file excel
        /// </summary>
        /// <returns>file excel chứa tất cả nhân viên</returns>
        [HttpGet("export-excel")]
        public IActionResult Export()
        {
            try
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
                    var forLoopIndex = rowStart+1;
                    foreach (var emp in allEmployees)
                    {
                        worksheet.Cells[forLoopIndex, 1].Value = tableIndex;
                        worksheet.Cells[forLoopIndex, 2].Value = CommonMethods.GetEmptyStringIfNull(emp.EmployeeCode);
                        worksheet.Cells[forLoopIndex, 3].Value = CommonMethods.GetEmptyStringIfNull(emp.FullName);
                        worksheet.Cells[forLoopIndex, 4].Value = CommonMethods.GetEmptyStringIfNull(emp.GenderName);
                        worksheet.Cells[forLoopIndex, 5].Value = emp.DateOfBirth==null? "":emp.DateOfBirth;
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
                string excelName = $"Danh sách nhân viên-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// lấy ra danh sách nhân viên và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="size"></param>
        /// <param name="keyword"></param>
        /// <returns>danh sách nhân viên và các thông tin phân trang</returns>
        [HttpGet]
        public IActionResult GetPaging(int? pageIndex, int? size, string? keyword)
        {
            try
            {
                // set giá trị mặc định cho các tham số không bắt buộc
                if (pageIndex == null)
                {
                    pageIndex = int.Parse(Common.PageIndexDefault);
                }
                if (size == null)
                {
                    size = int.Parse(Common.PageSizeDefault);
                }
                if (keyword == null)
                {
                    keyword = "";
                }

                // lấy ra đối tương paging
                var paging = _employeeRepo.GetPaging((int)pageIndex, (int)size, keyword);
                // trả về đối tượng paging
                return Ok(paging);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xoá nhiều employee
        /// </summary>
        /// <param name="ids">mảng employee id</param>
        /// <returns>số bản ghi xoá thành công</returns>
        [HttpDelete]
        public IActionResult DeleteMany(Guid[] ids) // frombody
        {
            try
            {
                var res = _employeeRepo.DeleteMany(ids);
                return Ok(res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy ra mã code nhân viên mới
        /// </summary>
        /// <returns>New employee code</returns>
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string employeeCode = _employeeRepo.GetNewEmployeeCode();
                return Ok(employeeCode);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy ra tất cả nhân viên hiện có
        /// </summary>
        /// <returns>tất cả nhân viên</returns>
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var emps = _employeeRepo.GetAll();
                return Ok(emps);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nhân viên tương ứng</returns>
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var emp = _employeeRepo.GetById(id);
                return Ok(emp);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Tạo nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>1 nếu thành công</returns>
        [HttpPost]
        public IActionResult CreateOne([FromBody] Employee employee)
        {
            try
            {
                int res =
                 _employeeService.Insert(employee);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>1 nếu thành công</returns>
        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            try
            {
                int res = _employeeService.Update(emp);
                return Ok(res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xoá nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 nếu thành công</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                int res = _employeeRepo.Delete(id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        #endregion
    }
}
