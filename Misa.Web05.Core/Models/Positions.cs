namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp Vị trí
    /// Author: trinh quy cong - 03/07/22
    /// </summary>
    public class Positions:BaseEntity
    {
        #region Properties
        /// <summary>
        /// khoa chinh
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// ten vi tri
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
