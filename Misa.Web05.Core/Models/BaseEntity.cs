namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Đối tượng tổng quát
    /// Created by TQCONG 1/7/2022
    /// </summary>
    public class BaseEntity
    {
        #region Properties
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
        #endregion
    }
}
