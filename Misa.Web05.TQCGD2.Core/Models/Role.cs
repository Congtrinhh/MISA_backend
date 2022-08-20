using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class vai trò
    /// </summary>
    /// CreatedBy TQCONG 28/7/2022
    public class Role:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khoá chính
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Tên vai trò
        /// </summary>
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = string.Empty;
        #endregion
    }
}
