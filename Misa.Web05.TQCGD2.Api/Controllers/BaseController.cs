using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.TQCGD2.Core.Exceptions;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Models.Paging;
using Misa.Web05.TQCGD2.Core.Resources;

namespace Misa.Web05.TQCGD2.Api.Controllers
{
    /// <summary>
    /// Controller tổng quát
    /// Created by TQCONG 17/8/22
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase where MISAEntity : BaseEntity
    {
        #region Property
        protected readonly IBaseRepo<MISAEntity> Repo;

        protected readonly IBaseService<MISAEntity> Service;
        #endregion

        #region Constructor
        public BaseController(IBaseRepo<MISAEntity> repo, IBaseService<MISAEntity> service)
        {
            Repo = repo;
            Service = service;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xử lý exception
        /// </summary>
        /// <param name="e">exception</param>
        /// <returns>Đối tượng chứa thông tin lỗi</returns>
        /// CreatedBy TQCONG 17/8/22
        protected IActionResult HandleException(Exception e)
        {
            // log lỗi vào ELK ...
            Console.WriteLine(e.Message);

            var errorCode = 500;

            // khởi tạo đối tượng thông báo lỗi
            //var errorMessage = new ErrorMessageResponse(userMsg: e.Message, devMsg: ExceptionErrorMessage.DevMessage500);
            var errorMessage = new ErrorMessageResponse();

            // lỗi do client 
            if (e is MISAValidationException)
            {
                errorCode = 400;
                errorMessage.UserMsg = ExceptionErrorMessage.UserMessage400;
                // lấy mảng erorr message từ exception hiện tại và gán vào phần data của response trả về
                errorMessage.Data = (List<string>)(e.Data[Common.ErrorFieldName] ?? new List<string>());
            }
            // lỗi do server
            else
            {
                errorCode = 500;
                errorMessage.UserMsg = e.Message ?? ExceptionErrorMessage.UserMessage500;
                errorMessage.DevMsg = e.Message ?? ExceptionErrorMessage.DevMessage500;
            }

            // trả về status code với mã lỗi (400/500) và chi tiết lỗi
            return StatusCode(errorCode, errorMessage);
        }

        /// <summary>
        /// xoá entity dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 5/8/2022
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne([FromRoute] int id)
        {
            try
            {
                int rs = await Repo.DeleteAsync(id);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// cập nhật entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 5/8/2022
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id, [FromBody] MISAEntity entity)
        {
            try
            {
                int rs = await Service.UpdateAsync(entity);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// tạo 1 entity mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] MISAEntity entity)
        {
            try
            {
                int rs = await Service.InsertAsync(entity);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// lấy ra entity theo id của nó
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy TQCONG 3/8/2022
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                MISAEntity rs = await Repo.GetByIdAsync(id);
                return Ok(rs);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// lấy ra tất cả entity
        /// </summary>
        /// <returns></returns>
        /// CreatedBy TQCONG 3/8/2022
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await Repo.GetAllAsync();
                return Ok(list);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        #endregion


    }
}
