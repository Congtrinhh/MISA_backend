using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;

namespace Misa.Web05.TQCGD2.Api.Controllers
{
    /// <summary>
    /// Controller cho Department
    /// CreatedBy TQCONG 13/8/2022
    /// </summary>
    public class DepartmentsController : BaseController<Department>
    {
        #region Property
        #endregion
        #region Contructor
        public DepartmentsController(IDepartmentRepo repo, IBaseService<Department> service) : base(repo, service)
        {
        }
        #endregion
        #region Method
        #endregion
    }
}
