namespace Misa.Web05.Core.Models
{
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
