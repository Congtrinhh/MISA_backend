namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp Vị trí
    /// CreatedBy TQCONG - 03/07/2022
    /// </summary>
    public class Positions:BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }
        #endregion

        #region Constructor
        public Positions()
        {
            PositionId = Guid.NewGuid();
        }
        #endregion
    }
}
