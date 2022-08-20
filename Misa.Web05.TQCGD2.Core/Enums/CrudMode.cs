using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Enums
{
    /// <summary>
    /// Enum trạng thái của quá trình thêm/sửa/xoá (sử dụng toàn cục trong các hàm thêm/xoá)
    /// Created by TQCONG 18/8/22
    /// </summary>
    public enum CrudMode
    {
        /// <summary>
        /// trạng thái THÊM MỚI
        /// </summary>
        Add = 0,

        /// <summary>
        /// trạng thái CẬP NHẬT
        /// </summary>
        Update = 1,

        /// <summary>
        /// trạng thái XOÁ
        /// </summary>
        Delete = 2,
    }
}
