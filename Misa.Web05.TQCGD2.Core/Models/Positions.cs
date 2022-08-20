using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class vị trí công việc
    /// </summary>
    /// CreatedBy TQCONG 28/7/2022
    public class Positions:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khoá chính
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Tên vị trí công việc
        /// </summary>
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = string.Empty ;
        #endregion
    }
}
