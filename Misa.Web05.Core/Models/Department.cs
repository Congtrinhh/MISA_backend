namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp phòng ban
    /// Author: Trinh Quy Cong - 03/07/22
    /// </summary>
    public class Department:BaseEntity
    {
        #region Properties
        /// <summary>
        /// khoa chinh
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// ten phong ban
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
