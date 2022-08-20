using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Models.Paging;

namespace Misa.Web05.TQCGD2.Api.Controllers
{
    /// <summary>
    /// Controller cho Class User
    /// </summary>
    /// CreatedBy TQCONG 3/8/2022
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User>
    {
        #region Property
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        #endregion

        #region Contructor
        public UsersController(IUserRepo userRepo, IUserService userService) : base(userRepo, userService)
        {
            _userRepo = userRepo;
            _userService = userService;
        }
        #endregion

        #region Method

        /// <summary>
        /// Tạo nhiều người dùng
        /// </summary>
        /// <param name="users">Danh sách user</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 8/8/2022
        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<User> users)
        {
            try
            {
                var result = await _userService.InsertManyAsync(users);
                return Ok(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy ra user có lọc và phân trang
        /// </summary>
        /// <param name="paginationRequest">Đối tượng để lọc và phân trang</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 17/8/2022
        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] UserPaginationRequest paginationRequest)
        {
            try
            {
                PaginationResponse<User> response = await Repo.GetPagingAsync(paginationRequest);
                // tính lại totalPages trong PaginationResponse
                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// lấy ra mã user code mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy TQCONG 17/8/2022
        [HttpGet("newUserCode")]
        public async Task<IActionResult> GetNewUserCode()
        {
            try
            {
                var rs = await _userRepo.GetNewUserCodeAsync();
                return Ok(rs);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        #endregion
        
    }
}
