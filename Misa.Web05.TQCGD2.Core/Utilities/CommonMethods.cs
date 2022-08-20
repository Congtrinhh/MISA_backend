using Misa.Web05.TQCGD2.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Utilities
{
    /// <summary>
    /// Class tiện dụng
    /// </summary>
    /// CreatedBy TQCONG 03/08/2022
    public class CommonMethods
    {
        /// <summary>
        /// Validate email
        ///// </summary>
        /// <param name="email">Email</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        /// CreatedBy TQCONG 17/8/2022
        public static bool IsEmailValid(string email)
        {
            var patternEmail = $@"{Common.RegexEmail}";
            Regex regexEmail = new Regex(patternEmail);
            return regexEmail.IsMatch(email);
        }

        /// <summary>
        /// Validate số điện thoại
        /// </summary>
        /// <param name="phone">Số điện thoại</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        /// CreatedBy TQCONG 17/8/2022
        public static bool IsPhoneNumberValid(string phone)
        {
            var patternPhone = $@"{Common.RegexPhoneNumber}";
            Regex regexPhone = new Regex(patternPhone);
            return regexPhone.IsMatch(phone);
        }

        /// <summary>
        /// Trả về chuỗi trỗng để đảm bảo không gặp Exception do giá trị null
        /// </summary>
        /// <param name="value">Một chuỗi</param>
        /// <returns>Nếu chuỗi là null, trả về chuỗi rỗng; ngược lại, trả về chính chuỗi đó</returns>
        /// CreatedBy TQCONG 17/8/2022
        public static string GetEmptyStringIfNull(string? value)
        {
            if (value == null)
            {
                return ""; // or string.empty
            }
            return value;
        }

        /// <summary>
        /// validate user code
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns>hợp lệ: true; không hợp lệ: false</returns>
        public static bool IsUserCodeValid(string userCode)
        {
            var pattern = $@"{Common.RegexUserCode}";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(userCode);
        }

        /// <summary>
        /// validate họ tên
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>hợp lệ: true; không hợp lệ: false</returns>
        public static bool IsFullNameValid(string fullName)
        {
            var pattern = $@"{Common.RegexFullName}";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(fullName);
        }
    }

}
