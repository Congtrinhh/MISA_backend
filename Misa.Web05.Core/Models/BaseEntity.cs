namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Đối tượng tổng quát
    /// Created by Trinh Quy Cong 1/7/22
    /// </summary>
    public class BaseEntity
    {
        #region Properties
        /// <summary>
        /// ngay tao
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ngay sua
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// tao boi ai
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// sua boi ai
        /// </summary>
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
