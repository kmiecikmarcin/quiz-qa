using System.Collections.Generic;

namespace RestSharpQuiz.Models
{
    public class Response
    {
        public string message { get; set; }
        public string error { get; set; }
        public string token { get; set; }
        public List<ValidationError> validationError { get; set; }
    }

    public class ValidationError
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string confirmPassword { get; set; }
        public string userGender { get; set; }
        public string userVerification { get; set; }
        public string newUserEmail { get; set; }
        public string newUserPassword { get; set; }
        public string confirmNewUserPassword { get; set; }
    }
}