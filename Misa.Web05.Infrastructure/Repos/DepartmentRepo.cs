using Dapper;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Infrastructure.Repos
{
    /// <summary>
    /// Repo cho đối tượng Department để tương tác với database
    /// Created by TQCONG 5/7/2022
    /// </summary>
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
        
    }
}
