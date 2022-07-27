using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Resources;

namespace Misa.Web05.Api.Controllers
{
    /// <summary>
    /// Controller tổng quát
    /// Created by TQCONG 8/7/22
    /// </summary>
    public class BaseController: ControllerBase
    {
        #region Methods
        /// <summary>
        /// Xử lý exception
        /// </summary>
        /// <param name="e">exception</param>
        /// <returns>Đối tượng chứa thông tin lỗi</returns>
        /// CreatedBy TQCONG 8/7/22
        protected IActionResult HandleException(Exception e)
        {
            var errorCode = 500;

            // khởi tạo đối tượng thông báo lỗi
            var errorMessage = new ErrorMessage(userMsg: e.Message, devMsg: Core.Resources.ExceptionErrorMessage.DevMessage500);

            // lỗi do client 
            if (e is MISAValidationException)
            {
                errorCode = 400;
                errorMessage.UserMsg = Core.Resources.ExceptionErrorMessage.UserMessage400;
                errorMessage.Data = e.Data;
            }
            // lỗi do server
            else
            {
                errorCode = 500;
                errorMessage.UserMsg = Core.Resources.ExceptionErrorMessage.UserMessage500;
            }

            // trả về status code với mã lỗi (400/500) và chi tiết lỗi
            return StatusCode(errorCode, errorMessage);
        }
        #endregion
    }
}
