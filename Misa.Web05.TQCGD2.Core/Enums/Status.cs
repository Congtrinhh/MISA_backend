using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Enums
{
    /// <summary>
    /// Enum trạng thái của người dùng cho Class User
    /// </summary>
    /// CreatedBy TQCONG 29/7/2022
    public enum Status
    {
        /// <summary>
        ///  Chưa được kích hoạt
        /// </summary>
        NotActivated =0,

        /// <summary>
        /// Chờ kích hoạt
        /// </summary>
        Pending =1,

        /// <summary>
        /// Đang hoạt động
        /// </summary>
        Active = 2,

        /// <summary>
        /// Đã bị huỷ kích hoạt
        /// </summary>
        Deactivated = 3
    }
}
