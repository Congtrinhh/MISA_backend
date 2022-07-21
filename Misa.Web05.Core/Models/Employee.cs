using Misa.Web05.Core.Enums;

namespace Misa.Web05.Core.Models
{
    /// <summary>
    /// Lớp Nhân viên
    /// Author: Trinh Quy Cong - 03/07/22
    /// </summary>
    public class Employee:BaseEntity
    {
        #region Properties
        /// <summary>
        /// khoa chinh
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// ma nhan vien
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// ten nhan vien
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// gioi tinh
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// ngay sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// so dien thoai
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// dia chi
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// so chung minh nhan dan
        /// </summary>
        public string? IdentityNumber { set; get; }

        /// <summary>
        /// luong
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// ma phong ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// ma vi tri
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// ten ngan hang
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// tên vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// tên giới tính: nam, nữ, khác
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
        /// ngày tháng năm đăng ký cmnd
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// số tk ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// nới đăng ký cmnd
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
