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
    /// Controller cho class Employee
    /// Created by TQCONG 5/7/22
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
        /// Xuất khẩu danh sách tất cả nhân viên có trong DB ra 1 file excel
        /// </summary>
        /// <returns>File excel chứa tất cả nhân viên</returns>
        /// CreatedBy TQCONG 5/7/22
        [HttpGet("export-excel")]
        public IActionResult Export()
        {
            try
            {
                var stream = _employeeService.Export();

                string excelName = $"Danh sách nhân viên-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy ra danh sách nhân viên và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex">Trang cần lấy (bắt đầu từ 0)</param>
        /// <param name="size">Số item/trang</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <returns>Danh sách nhân viên và các thông tin phân trang</returns>
        /// CreatedBy TQCONG 5/7/22
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
        /// <param name="ids">Mảng employee id</param>
        /// <returns>Số bản ghi xoá thành công</returns>
        /// CreatedBy TQCONG 5/7/22
        [HttpDelete]
        public IActionResult DeleteMany(Guid[] ids)
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
        /// CreatedBy TQCONG 5/7/22
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
        /// <returns>Tất cả nhân viên</returns>
        /// CreatedBy TQCONG 5/7/22
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
        /// CreatedBy TQCONG 5/7/22
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
        /// <param name="employee">Đối tượng employee</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 5/7/22
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
        /// <param name="emp">Đối tượng employee</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 5/7/22
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
        /// CreatedBy TQCONG 5/7/22
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
