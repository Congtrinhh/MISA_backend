using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;

namespace Misa.Web05.Api.Controllers
{
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
        /// get list of department
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
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
