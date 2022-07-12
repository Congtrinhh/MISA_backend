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
    /// Class tổng quát cho đối tượng Department
    /// Created by trinh quy cong 5/7/22
    /// </summary>
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
        
    }
}
