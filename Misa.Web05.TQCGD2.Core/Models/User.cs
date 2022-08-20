using Misa.Web05.TQCGD2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class người dùng
    /// </summary>
    /// CreatedBy TQCONG 29/7/2022
    public class User : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khoá chính
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Mã người dùng (dùng để hiển thị)
        /// </summary>
        public string UserCode { get; set; } = String.Empty;

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string FullName { get; set; } = String.Empty;

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Trạng thái người dùng
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public Department? Department { get; set; }

        /// <summary>
        /// Vị trí công việc
        /// </summary>
        public Positions? Position { get; set; }

        /// <summary>
        /// Các vai trò
        /// </summary>
        public List<Role>? Roles { get; set; }

        /// <summary>
        /// Tên phòng ban (chỉ dùng cho việc hiển thị)
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tên vị trí công việc (chỉ dùng cho việc hiển thị)
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Danh sách tên vai trò được ngăn cách bởi dấu "," (cho việc hiển thị)
        /// </summary>
        public string? RoleNames { get; set; }

        /// <summary>
        /// Id phòng ban (cho việc thêm/cập nhật - vì không thể truyền cả object Department vào Dapper)
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Id vị trí công việc (cho việc thêm/cập nhật - vì không thể truyền cả object Positions vào Dapper)
        /// </summary>
        public int? PositionId { get; set; }
        #endregion
    }
}
