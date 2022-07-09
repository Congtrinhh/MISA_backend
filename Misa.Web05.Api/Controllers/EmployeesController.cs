using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;

namespace Misa.Web05.Api.Controllers
{
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
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string employeeCode = _employeeRepo.getNewEmployeeCode();
                return Ok(employeeCode);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
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

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]Guid id)
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
