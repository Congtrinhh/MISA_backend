using Microsoft.AspNetCore.Http;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// nhận vào file exel
        /// trả về danh sách nhân viên import được
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IEnumerable<Employee> Import(IFormFile file);
    }
}
