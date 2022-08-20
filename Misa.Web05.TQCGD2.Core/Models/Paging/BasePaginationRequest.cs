using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Web05.TQCGD2.Core.Resources;
using Newtonsoft.Json;

namespace Misa.Web05.TQCGD2.Core.Models.Paging
{
    /// <summary>
    /// Class base cho việc phân trang gồm các thông tin cơ bản
    /// </summary>
    /// CreatedBy TQCONG 5/8/2022
    public class BasePaginationRequest
    {
        /// <summary>
        /// Trang cần lấy - trang nhỏ nhất có giá trị là 1
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Số bản ghi/trang
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = int.Parse(Common.PageSizeDefault);
    }
}
