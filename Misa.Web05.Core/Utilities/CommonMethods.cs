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
    /// Created by Trinh Quy Cong 9/7/22
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// Validate email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        public static bool IsEmailValid(string email)
        {
            var patternEmail = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            Regex regexEmail = new Regex(patternEmail);
            return regexEmail.IsMatch(email);
        }

        /// <summary>
        /// Validate số điện thoại
        /// </summary>
        /// <param name="phone">số điện thoại</param>
        /// <returns>true nếu hợp lệ;ngược lại false</returns>
        public static bool IsPhoneNumberValid(string phone)
        {
            var patternPhone = @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
            Regex regexPhone = new Regex(patternPhone);
            return regexPhone.IsMatch(phone);
        }

        /// <summary>
        /// return string to make sure exception not occur
        /// </summary>
        /// <param name="value">a string</param>
        /// <returns>an empty string if the passed value is null</returns>
        public static string GetEmptyStringIfNull(string value)
        {
            if (value == null)
            {
                return "";
            }
            return value;
        }
    }
}
