using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class đại diện cho bảng trung gian của 2 bảng User và Role trong database
    /// </summary>
    /// CreatedBy TQCONG 4/8/2022
    public class UserRole:BaseEntity
    {
        #region Constructor
        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        #endregion

        #region Property

        /// <summary>
        /// Id của user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id của role
        /// </summary>
        public int RoleId { get; set; }
        #endregion

    }
}
