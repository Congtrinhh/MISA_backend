using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class phòng ban
    /// </summary>
    /// CreatedBy TQCONG 28/7/2022
    public class Department:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khoá chính
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        #endregion
    }
}
