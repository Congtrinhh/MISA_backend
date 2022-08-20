using Misa.Web05.TQCGD2.Core.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Core.Exceptions
{
    /// <summary>
    /// Class custom exception
    /// Created by TQCONG 17/8/22
    /// </summary>
    public class MISAValidationException : Exception
    {
        #region Properties
       
        #endregion

        #region Constructor
        public MISAValidationException(List<string> errors)
        {
            this.Data.Add(Common.ErrorFieldName, errors);
        }
        #endregion

        #region Methods
        #endregion
    }
}
