using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;

namespace Misa.Web05.Api.Controllers
{
    /// <summary>
    /// Created by trinh quy cong 5/7/22
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        #region Properties
        IDepartmentService _departmentService;
        IDepartmentRepo _departmentRepo;

        #endregion
        #region Constructor
        public DepartmentsController(IDepartmentService departmentService, IDepartmentRepo departmentRepo)
        {
            _departmentService = departmentService;
            _departmentRepo = departmentRepo;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy ra danh sách department và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="size"></param>
        /// <param name="keyword"></param>
        /// <returns>Đối tượng paging chứa danh sách phòng ban và thông tin phân trang</returns>
        [HttpGet]
        public IActionResult GetPaging(int? pageIndex, int? size, string? keyword)
        {
            try
            {
                if (pageIndex == null)
                {
                    pageIndex = 0;
                }
                if (size == null)
                {
                    size = 10;
                }
                if (keyword == null)
                {
                    keyword = "";
                }

                var paging = _departmentRepo.GetPaging((int)pageIndex, (int)size, keyword);
                return Ok(paging);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        /// <summary>
        /// lấy ra tất cả phòng ban hiện có
        /// </summary>
        /// <returns>Tất cả phòng ban</returns>
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var departments = _departmentRepo.GetAll();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// lấy ra phòng ban theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Phòng ban tương ứng</returns>
        [HttpGet("{id}")]
        public IActionResult getOne(Guid id)
        {
            try
            {
                var dep = _departmentRepo.GetById(id);
                return Ok(dep);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Tạo phòng ban
        /// </summary>
        /// <param name="d"></param>
        /// <returns>trả về 1 nếu thành công</returns>
        [HttpPost]
        public IActionResult CreateOne(Department d)
        {
            try
            {
                var res = _departmentService.Insert(d);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="dep"></param>
        /// <returns>Trả về 1 nếu thành công</returns>
        [HttpPut]
        public IActionResult Update(Department dep)
        {
            try
            {
                int res = _departmentService.Update(dep);
                return Ok(res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xoá phòng ban
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về 1 nếu thành công</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                int res = _departmentRepo.Delete(id);
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
