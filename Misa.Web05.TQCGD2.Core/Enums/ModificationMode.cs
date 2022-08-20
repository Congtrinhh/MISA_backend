using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Enums
{
    /// <summary>
    /// Enum xác định cần làm gì với dữ liệu được gửi lên từ client (có trong từng entity)
    /// </summary>
    /// CreatedBy TQCONG 5/8/2022
    public enum ModificationMode
    {
        /// <summary>
        /// Không làm gì
        /// </summary>
        Nothing = 0,

        /// <summary>
        /// Dữ liệu sẽ được thêm mới vào database
        /// </summary>
        Insert = 1,

        /// <summary>
        /// Dữ liệu sẽ được loại bỏ khỏi database
        /// </summary>
        Remove = 2,

        /// <summary>
        /// Dữ liệu sẽ được cập nhật
        /// </summary>
        Update = 3,
    }
}
