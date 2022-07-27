namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp phòng ban
    /// CreatedBy TQCONG - 03/07/2022
    /// </summary>
    public class Department:BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        #endregion

        #region Constructor
        public Department()
        {
            DepartmentId = Guid.NewGuid();
        }

        #endregion
    }
}
