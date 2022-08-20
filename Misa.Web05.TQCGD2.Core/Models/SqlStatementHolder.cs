using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Models
{
    /// <summary>
    /// Class gồm các câu lệnh sql nhỏ như SELECT, WHERE,..
    /// </summary>
    /// CreatedBy TQCONG 8/8/2022
    public class SqlStatementHolder
    {
        #region Property
        /// <summary>
        /// Nội dung của mệnh đề SELECT
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// Nội dung của mệnh đề WHERE
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// Nội dung của mệnh đề FROM
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Nội dung của mệnh đề ORDER BY
        /// </summary>
        public string OrderBy { get; set; }
        #endregion
    }
}
