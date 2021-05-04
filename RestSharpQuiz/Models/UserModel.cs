using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpQuiz.Models
{
    public class Response
    {
        public Message messages { get; set; }
        public List<ValidationErrors> validationErrors { get; set; }
    }
    public class Message
    {
        public string message { get; set; }
    }
    public class ValidationErrors
    {
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string confirm_password { get; set; }
        public string user_gender { get; set; }
        public string user_verification { get; set; }
    }
}