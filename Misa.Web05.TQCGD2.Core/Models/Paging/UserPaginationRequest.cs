using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models.Paging
{
    /// <summary>
    /// Class chứa thông tin phân trang cho User
    /// bao gồm các thông tin cho việc filtering, sorting
    /// </summary>
    /// CreatedBy TQCONG 5/8/2022
    public class UserPaginationRequest : BasePaginationRequest
    {
        /// <summary>
        /// Tìm kiếm từ khoá trong họ tên
        /// </summary>
        [JsonProperty("fullName")]
        public string? FullName { get; set; } = string.Empty;

        /// <summary>
        /// Tìm kiếm theo id vai trò
        /// </summary>
        [JsonProperty("roleId")]
        public int? RoleId { get; set; } = -1;
    }
}
