using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpQuiz.Models
{
    public class Response
    {
        public ValidationResponse validationResponse { get; set; }

    public class ValidationResponse
    {
        public string value { get; set; }
        public string msg { get; set; }
        public string param { get; set; }
        public string location { get; set; }
    }

    public class ResponseMessage
    {
        public string Message { get; set; }
    }

    public class ResponseError
    {
        public string Error { get; set; }
    }
}
