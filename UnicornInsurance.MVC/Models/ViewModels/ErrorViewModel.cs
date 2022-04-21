using System;

namespace UnicornInsurance.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public Error Error { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
    }
}
