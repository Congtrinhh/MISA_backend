using Misa.Web05.Core.Enums;

namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp Nhân viên
    /// CreatedBy TQCONG - 03/07/2022
    /// </summary>
    public class Employee:BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { set; get; }

        /// <summary>
        /// Lương
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên giới tính: nam, nữ, khác
        /// </summary>
        public string? GenderName { get
            {
                switch (Gender)
                {
                    case Enums.Gender.Male:
                        return "Nam";
                    case Enums.Gender.Female:
                        return "Nữ";
                    default: return "Khác";
                }
            }
        }

        /// <summary>
        /// Ngày tháng năm đăng ký cmnd
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Nơi đăng ký chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }

        #endregion

        #region Constructor
        public Employee()
        {
            EmployeeId = Guid.NewGuid();
        }
        #endregion
    }
}
