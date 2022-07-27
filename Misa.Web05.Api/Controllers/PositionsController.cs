using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;

namespace Misa.Web05.Api.Controllers
{
    /// <summary>
    /// Controller cho class Positions
    /// Created by TQCONG - 5/7/22
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BaseController
    {
        #region Properties
        private IPositionsRepo _positionsRepo;
        private IPositionsService _positionsService;
        #endregion

        #region Constructor
        public PositionsController(IPositionsRepo positionsRepo, IPositionsService positionsService)
        {
            _positionsRepo = positionsRepo;
            _positionsService = positionsService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra danh sách vị trí và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex">Trang cần lấy (bắt đầu từ 0)</param>
        /// <param name="size">Số bản ghi/trang</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <returns>Danh sách vị trí và các thông tin phân trang</returns>
        /// CreatedBy TQCONG - 5/7/22
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

                var paging = _positionsRepo.GetPaging((int)pageIndex, (int)size, keyword);
                return Ok(paging);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy ra tất cả position hiện có
        /// </summary>
        /// <returns>Tất cả position</returns>
        /// CreatedBy TQCONG - 5/7/22
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var positions = _positionsRepo.GetAll();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Lấy ra position theo id
        /// </summary>
        /// <param name="id">Id position</param>
        /// <returns>Position tương ứng</returns>
        /// CreatedBy TQCONG - 5/7/22
        [HttpGet("{id}")]
        public IActionResult getOne(Guid id)
        {
            try
            {
                var pos = _positionsRepo.GetById(id);
                return Ok(pos);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Tạo position
        /// </summary>
        /// <param name="pos">Đối tượng position</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG - 5/7/22
        [HttpPost]
        public IActionResult CreateOne(Positions pos)
        {
            try
            {
                var res = _positionsService.Insert(pos);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Cập nhật position
        /// </summary>
        /// <param name="pos">Đối tượng position</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG - 5/7/22
        [HttpPut]
        public IActionResult Update(Positions pos)
        {
            try
            {
                int res = _positionsService.Update(pos);
                return Ok(res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xoá position
        /// </summary>
        /// <param name="id">Id position</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG - 5/7/22
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                int res = _positionsRepo.Delete(id);
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
