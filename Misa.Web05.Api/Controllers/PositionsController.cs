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
        /// lấy ra danh sách vị trí và các thông tin phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="size"></param>
        /// <param name="keyword"></param>
        /// <returns>danh sách vị trí và các thông tin phân trang</returns>
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
        /// lấy ra tất cả position hiện có
        /// </summary>
        /// <returns>tất cả position</returns>
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
        /// <param name="id"></param>
        /// <returns>Position tương ứng</returns>
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
        /// <param name="pos"></param>
        /// <returns>1 nếu thành công</returns>
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
        /// <param name="pos"></param>
        /// <returns>1 nếu thành công</returns>
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
        /// <param name="id"></param>
        /// <returns>1 nếu thành công</returns>
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
