using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models.Paging
{
    /// <summary>
    /// Class chứa thông tin phân trang trả về cho client
    /// </summary>
    /// <typeparam name="T">Một Class kế thừa từ BaseEntity - ví dụ: User/Department,...</typeparam>
    /// CreatedBy TQCONG 5/8/2022
    public class PaginationResponse<T> where T : BaseEntity
    {
        #region Property

        /// <summary>
        /// Danh sách entity
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi khi chưa phân trang
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Số item/trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Trang hiện tại (tính từ 1)
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Có thể nhảy về trang trước không
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Có thể nhảy đến trang tiếp theo không
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// số thứ tự của bản ghi bắt đầu
        /// </summary>
        public int RecordStart { get; set; }

        /// <summary>
        /// số thứ tự bản ghi kết thúc
        /// </summary>
        public int RecordEnd { get; set; }
        #endregion

        #region Constructor

        public PaginationResponse(List<T> data, int totalCount, int pageSize, int currentPage)
        {
            Data = data;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            HasPrevious = CurrentPage > 1;
            HasNext = CurrentPage < TotalPages;

            RecordStart = (currentPage - 1) * pageSize + 1;
            RecordEnd = HasNext ? (RecordStart + pageSize -1) : (RecordStart + data.Count -1);
        }
        #endregion
    }
}
