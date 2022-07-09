using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Exceptions
{
    public class MISAValidationException : Exception
    {
        public string? ErrorMessage { get; set; }
        public IDictionary Errors { get; set; }

        public MISAValidationException(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public MISAValidationException(List<string> errors)
        {
            Errors = new Dictionary<string, object>();
            this.Errors.Add("errors", errors);
        }

        public override IDictionary Data => this.Errors;

        public override string Message => this.ErrorMessage;
    }
}
