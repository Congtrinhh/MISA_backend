using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;

namespace Misa.Web05.Api.Controllers
{
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
        /// get list of positions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
