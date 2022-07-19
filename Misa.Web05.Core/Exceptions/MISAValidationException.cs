using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Exceptions
{
    /// <summary>
    /// custom exception class
    /// Created by Trinh Quy Cong 6/7/22
    /// </summary>
    public class MISAValidationException : Exception
    {
        #region Properties
        /// <summary>
        /// error message
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// đối tượng chứa danh sách thông báo lỗi
        /// </summary>
        public IDictionary Errors { get; set; }
        #endregion

        #region Constructor
        public MISAValidationException(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public MISAValidationException(List<string> errors)
        {
            Errors = new Dictionary<string, object>();
            this.Errors.Add(Resources.Common.ErrorFieldName, errors);
        }
        #endregion

        #region Methods
        /// <summary>
        /// ghi đè property Data của đối tượng Exception để trả về mảng thông báo lỗi
        /// </summary>
        public override IDictionary Data => this.Errors;

        /// <summary>
        /// ghi đè property Message của đối tượng Exception để trả về thông báo lỗi
        /// </summary>
        public override string Message => this.ErrorMessage;
        #endregion
    }
}
