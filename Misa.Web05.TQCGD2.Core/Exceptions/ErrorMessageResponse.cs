using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Exceptions
{
    /// <summary>
    /// Message object để trả về cho client khi có lỗi xảy ra
    /// Created by TQCONG - 17/8/2022
    /// </summary>
    public class ErrorMessageResponse
    {
        #region Properties
        /// <summary>
        /// message for user
        /// </summary>
        [JsonPropertyName("userMsg")]
        public string UserMsg { get; set; }

        /// <summary>
        /// message for developer
        /// </summary>
        [JsonPropertyName("devMsg")]
        public string DevMsg { get; set; }

        /// <summary>
        /// internal error code - for MISA
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// id of error in the log file
        /// </summary>
        [JsonPropertyName("traceId")]
        public int TraceId { get; set; }

        /// <summary>
        /// contains details of errors
        /// </summary>
        [JsonPropertyName("data")]
        public List<string> Data { get; set; }
        #endregion

        #region Contructor
        public ErrorMessageResponse(string userMsg, string devMsg) : this()
        {
            this.UserMsg = userMsg;
            this.DevMsg = devMsg;
        }

        public ErrorMessageResponse()
        {
        }

        public ErrorMessageResponse(string userMsg, string devMsg, string errorCode, int traceId)
        {
            this.UserMsg = userMsg;
            this.DevMsg = devMsg;
            this.ErrorCode = errorCode;
            this.TraceId = traceId;
        }
        #endregion
    }
}
