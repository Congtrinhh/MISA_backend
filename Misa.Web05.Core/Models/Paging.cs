using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Chứa thông tin phân trang trả về cho client
    /// Created by Trinh Quy Cong 9/7/22
    /// </summary>
    public class Paging
    {
        #region Properties
        /// <summary>
        /// dữ liệu trả về (danh sách nhân viên, phòng ban,..)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// tổng số bản ghi trả về
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// trang hiện tại, tính từ 0
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// thứ tự bản ghi bắt đầu
        /// </summary>
        public int RecordStart { get; set; }

        /// <summary>
        /// thứ tự bản ghi kết thúc
        /// </summary>
        public int RecordEnd { get; set; }

        
        #endregion

        #region Constructor
        public Paging()
        {
        }

        public Paging(object data, int totalRecords, int recordStart, int recordEnd)
        {
            Data = data;
            TotalRecords = totalRecords;
            RecordStart = recordStart;
            RecordEnd = recordEnd;
        }

        #endregion
    }
}
