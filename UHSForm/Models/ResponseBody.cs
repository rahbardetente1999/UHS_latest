using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class ResponseBody
    {
        public object resultObj { get; set; }
        public object returnCode { get; set; }
        public object errorCode { get; set; }
        public object errorMessage { get; set; }
        public object error { get; set; }
        public object validationErrors { get; set; }
        public object hasValidationError { get; set; }
        public object hasError { get; set; }

    }
}