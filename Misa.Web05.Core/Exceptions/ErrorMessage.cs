using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Exceptions
{
    /// <summary>
    /// Message object to return to client when exception occurs
    /// Created by Trinh Quy Cong - 5/7/2022
    /// </summary>
    public class ErrorMessage
    {
        #region Properties

        /// <summary>
        /// message for user
        /// </summary>
        public string userMsg { get; set; }

        /// <summary>
        /// message for developer
        /// </summary>
        public string devMsg { get; set; }

        /// <summary>
        /// internal error code - for MISA
        /// </summary>
        public string errorCode { get; set; }

        /// <summary>
        /// id of error in the log file
        /// </summary>
        public int traceId { get; set; }

        /// <summary>
        /// contains details of errors
        /// </summary>
        public object data { get; set; }
        #endregion

        #region Contructor
        public ErrorMessage(string userMsg, string devMsg):this()
        {
            this.userMsg = userMsg;
            this.devMsg = devMsg;
        }

        public ErrorMessage()
        {
        }

        public ErrorMessage(string userMsg, string devMsg, string errorCode, int traceId)
        {
            this.userMsg = userMsg;
            this.devMsg = devMsg;
            this.errorCode = errorCode;
            this.traceId = traceId;
        }
        #endregion
    }
}
