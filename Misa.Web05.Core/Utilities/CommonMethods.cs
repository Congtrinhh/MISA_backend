using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Utilities
{
    /// <summary>
    /// Chứa các method thường dùng như validate email,..
    /// Created by TQCONG 9/7/22
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// Validate email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        /// CreatedBy TQCONG 9/7/2022
        public static bool IsEmailValid(string email)
        {
            var patternEmail = $@"{Resources.Common.RegexEmail}";
            Regex regexEmail = new Regex(patternEmail);
            return regexEmail.IsMatch(email);
        }

        /// <summary>
        /// Validate số điện thoại
        /// </summary>
        /// <param name="phone">Số điện thoại</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        /// CreatedBy TQCONG 9/7/2022
        public static bool IsPhoneNumberValid(string phone)
        {
            var patternPhone = $@"{Resources.Common.RegexPhoneNumber}";
            Regex regexPhone = new Regex(patternPhone);
            return regexPhone.IsMatch(phone);
        }

        /// <summary>
        /// Trả về chuỗi trỗng để đảm bảo không gặp Exception do giá trị null
        /// </summary>
        /// <param name="value">Một chuỗi</param>
        /// <returns>Nếu chuỗi là null, trả về chuỗi rỗng; ngược lại, trả về chính chuỗi đó</returns>
        /// CreatedBy TQCONG 9/7/2022
        public static string GetEmptyStringIfNull(string? value)
        {
            if (value == null)
            {
                return "";
            }
            return value;
        }
    }
}
