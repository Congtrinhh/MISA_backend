using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Exceptions;

namespace Misa.Web05.Api.Controllers
{
    public class BaseController: ControllerBase
    {
        protected IActionResult HandleException(Exception e)
        {
            var errorCode = 500;
            var errorMessage = new ErrorMessage(userMsg: e.Message, devMsg: "Có lỗi xảy ra với hệ thống của chúng ta");

            if (e is MISAValidationException)
            {
                errorCode = 400;
                errorMessage.userMsg = "Dữ liệu đầu vào không hợp lệ";
                errorMessage.data = e.Data;
            } else
            {
                errorMessage.userMsg = "Có lỗi xảy ra với hệ thống";
                errorCode = 500;
            }

            return StatusCode(errorCode, errorMessage);
        }
    }
}
