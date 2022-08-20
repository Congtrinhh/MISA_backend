using Misa.Web05.TQCGD2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class base gồm các thuộc tính chung cho các entity khác kế thừa
    /// </summary>
    /// CreatedBy TQCONG 28/7/2022
    public class BaseEntity
    {
        #region Property
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Xác định xem sẽ làm gì với dữ liệu được gửi lên từ client
        /// </summary>
        public ModificationMode ModificationMode { get; set; }

        #endregion
    }
}
