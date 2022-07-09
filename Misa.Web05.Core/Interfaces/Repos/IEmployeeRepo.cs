using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Repos
{
    public interface IEmployeeRepo:IBaseRepo<Employee>
    {
        /// <summary>
        /// insert list employees from excel file import
        /// return the number of inserted employees
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        int Import(List<Employee> employees);

        /// <summary>
        /// get new employee code to insert new employee
        /// </summary>
        /// <returns></returns>
        string getNewEmployeeCode();
    }
}
