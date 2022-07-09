using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Misa.Web05.Infrastructure.Repos
{
    /// <summary>
    /// Repo cho employee
    /// Created by Trinh Quy Cong 9/7/2022
    /// </summary>
    public class EmployeeRepo : BaseRepo<Employee>, IEmployeeRepo
    {
        public string getNewEmployeeCode()
        {
            using (base.Conn = new MySqlConnection(base.SqlConnectionString))
            {
                var res = Conn.QueryFirstOrDefault<string>(
                    "Proc_GetNewEmployeeCode", commandType: System.Data.CommandType.StoredProcedure
                    );
                return res;
            }
        }

        public int Import(List<Employee> employees)
        {
            return 0;
        }
    }
}