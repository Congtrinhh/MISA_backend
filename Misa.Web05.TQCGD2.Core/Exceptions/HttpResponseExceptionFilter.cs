using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Misa.Web05.TQCGD2.Core.Exceptions
{
    public class HttpResponseExceptionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter, IOrderedFilter
    {

        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Exception is MisaResponseException misaResponseException)
            //{
            //    context.Result = new ObjectResult(misaResponseException)
            //    {
            //        StatusCode = misaResponseException.StatusCode
            //    };

            //    context.ExceptionHandled = true;
            //}
        }
    }
}
