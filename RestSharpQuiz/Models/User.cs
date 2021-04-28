using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpQuiz.Models
{
    public class User
    {
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string confirm_password { get; set; }
        public string user_gender { get; set; }
        public bool user_verification { get; set; }

        public User(string user_email, string user_password, string confirm_password, string user_gender, bool user_verification)
        {
            this.user_email = user_email;
            this.user_password = user_password;
            this.confirm_password = confirm_password;
            this.user_gender = user_gender;
            this.user_verification = user_verification;
        }

        public string GenerateEmail()
        {
            Random rand = new Random();

            user_email = $"userLogin{rand.Next(0, 10000)}";

            return user_email;
        }

        public User CreateUser(User user)
        {
            if(user.user_email == null)
            {
                user.user_email = GenerateEmail();
            }

            return user;
        }
    }
}
